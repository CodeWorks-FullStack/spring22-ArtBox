CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS art(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title VARCHAR(255) DEFAULT "Untitled",
  imgUrl TEXT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (creatorId)
    REFERENCES accounts(id)
    ON DELETE CASCADE

) default charset utf8;


CREATE TABLE IF NOT EXISTS comments(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  body TEXT NOT NULL,
  artId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (creatorId)
    REFERENCES accounts(id)
    ON DELETE CASCADE,

  FOREIGN KEY (artId)
    REFERENCES art(id)
    ON DELETE CASCADE

) default charset utf8;



CREATE TABLE IF NOT EXISTS purchases(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  artId INT NOT NULL,
  accountId VARCHAR(255) NOT NULL,
  price DECIMAL(6, 2) NOT NULL,
  delivered TINYINT NOT NULL DEFAULT 0,

  FOREIGN KEY (artId)
    REFERENCES art(id)
    ON DELETE CASCADE,

  FOREIGN KEY (accountId)
    REFERENCES accounts(id)
    ON DELETE CASCADE
)default charset utf8;






/* Playground */
INSERT INTO purchases
(artId, accountId, price)
VALUES
(1, "628e650a2dbad264cba2fc33", 1097.11);


/* get all through relationship */

SELECT
 a.*,
 p.price,
 p.delivered,
 p.id AS purchaseId
FROM purchases p
JOIN art a ON p.artId = a.id
WHERE p.accountId = "jd123";


/* WHEN POPULATING ON THE CHILD IN THE MANY TO MANY */
SELECT
 act.*,
 a.*,
 p.price,
 p.delivered,
 p.id AS purchaseId
FROM purchases p
JOIN art a ON p.artId = a.id
JOIN accounts act ON a.creatorId = act.id
WHERE p.accountId = "jd123";












INSERT INTO accounts
(id, name)
VALUES
("jd123", "JaneDoe");






/* POST */
INSERT INTO art
(title, imgUrl, creatorId)
VALUES
("Mona Lisa", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ec/Mona_Lisa%2C_by_Leonardo_da_Vinci%2C_from_C2RMF_retouched.jpg/270px-Mona_Lisa%2C_by_Leonardo_da_Vinci%2C_from_C2RMF_retouched.jpg", "jd123");

/* GET ALL */
SELECT * FROM art;

/* GET BY ID** */
SELECT * FROM art WHERE id = 1;
SELECT * FROM art WHERE title = "Vigo the Carpathian" AND id = 3;
SELECT * FROM art WHERE title LIKE "%bo%";

SELECT * FROM art WHERE creatorId = "jd123";

/* NOW WITH POPULATE! */
/* GETALL */
SELECT
  ar.*,
  ac.*
FROM art ar
JOIN accounts ac ON ar.creatorId = ac.id;






/* PUT */
UPDATE art
SET
  title = "Rosie Riveter"
WHERE id = 3;




/* DELETE */
DELETE FROM art WHERE id = 4 LIMIT 1;


/* DANGER ZONE */
/* REMOVE ALL DATA FROM A TABLE */
DELETE FROM art;

/* REMOVE TABLE */
DROP TABLE art;


/* FULL BAD NEVER DO  */
DROP DATABASE MarkDb;

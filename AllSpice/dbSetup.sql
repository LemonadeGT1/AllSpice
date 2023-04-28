-- Active: 1682439242288@@SG-CodeWorks-7504-mysql-master.servers.mongodirector.com@3306@AllSpice

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8mb4 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        title VARCHAR(255) NOT NULL,
        instructions VARCHAR(2047) NOT NULL,
        img VARCHAR(255) NOT NULL,
        category VARCHAR(63) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

DROP TABLE recipes;

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        quantity VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) DEFAULT CHARSET utf8mb4;

CREATE TABLE
    IF NOT EXISTS favorites(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        accountId VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) DEFAULT CHARSET utf8mb4;

INSERT INTO
    recipes(
        title,
        instructions,
        img,
        category,
        creatorId
    )
VALUES (
        "Lemon Asparagus Pasta",
        "Fill a large pot with lightly salted water and bring to a boil. Stir in penne and return to a boil. Cook pasta uncovered, stirring occasionally, for about 8 minutes. Penne will still be slightly firm.

Stir asparagus into the pot with the boiling penne pasta and bring back to a boil; cook until pasta is tender and asparagus is softened but still bright green, about 3 more minutes. Drain pasta and asparagus.

Place penne and asparagus into a large bowl; lightly stir in lemon zest, lemon juice, Parmesan cheese, olive oil, salt, and black pepper until thoroughly combined.",
        "https://www.allrecipes.com/thmb/mDc1ysZI0oG5sK4TPwTjdFcZ0B0=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/957488-4a13ab05962e44a89f740958701e01a4.jpg",
        "Pasta",
        "642cae5df1fcddc1a6a32924"
    );

INSERT INTO
    ingredients(name, quantity, recipeId)
VALUES (
        "Package linguine pasta",
        "1/2 (16 ounce)",
        2
    ), (
        "Large head broccoli, trimmed and chopped",
        "1",
        2
    ), (
        "Pine nuts, chopped",
        "1/2 cup",
        2
    ), (
        "Garlic cloves, crushed",
        "5",
        2
    ), (
        "Extra-virgin olive oil , divided",
        "5 tablespoons",
        2
    ), (
        "Lemon-pepper seasoning",
        "1/2 teaspoon",
        2
    ), (
        "Chicken soup base",
        "1/4 cup",
        2
    ), (
        "Extra virgin olive oil",
        "1 teaspoon",
        2
    ), (
        "Medium tomatoes, chopped",
        "2",
        2
    ), (
        "Red pepper flakes",
        "1 teaspoon",
        2
    ), ("Salt", "to taste", 2), ("Butter", "1 tablespoon", 2), (
        "Balsamic vinegar",
        "1 dash (optional)",
        2
    );

SELECT r.*, i.*, creator.name
FROM recipes r
    JOIN ingredients i ON i.recipeId = r.id
    JOIN accounts creator ON r.creatorId = creator.id;

SELECT r.*, creator.name
FROM recipes r
    JOIN accounts creator ON r.creatorId = creator.id;

-- WHERE r.id = LAST_INSERT_ID();
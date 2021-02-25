
-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );


-- CREATE TABLE restaurants
-- (
--   id INT NOT NULL AUTO_INCREMENT,
--   ownerId VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL UNIQUE,
--   description VARCHAR(255) NOT NULL,
--   location VARCHAR(255) NOT NULL,

--   PRIMARY KEY (id),

--   FOREIGN KEY (ownerId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE
-- );

-- ALTER TABLE restaurants DROP COLUMN description


-- CREATE TABLE reviews
-- (
--    id INT NOT NULL AUTO_INCREMENT,
--    restaurantId INT NOT NULL,
--    title VARCHAR(255) NOT NULL,
--    body VARCHAR(255) NOT NULL,
--    ownerId VARCHAR(255) NOT NULL,
--    published TINYINT NOT NULL DEFAULT 0,
--    rating INT CHECK (rating >= 0 AND rating < 6),

--   PRIMARY KEY (id),

--   FOREIGN KEY (ownerId)
--    REFERENCES profiles(id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (restaurantId)
--    REFERENCES restaurants(id)
--    ON DELETE CASCADE
-- );

DROP INDEX namelocation on restaurants;
ALTER TABLE restaurants ADD UNIQUE `namelocation`(`name`, `location`);
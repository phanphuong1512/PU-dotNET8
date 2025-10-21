-- Xoá nếu đã tồn tại (cho dễ test)
DROP TABLE IF EXISTS reviews, order_details, orders, products, categories, users CASCADE;

-- 1️⃣ Bảng users
CREATE TABLE users (
                       user_id SERIAL PRIMARY KEY,
                       username VARCHAR(100) NOT NULL UNIQUE,
                       email VARCHAR(150) NOT NULL UNIQUE,
                       password_hash TEXT NOT NULL
);

-- 2️⃣ Bảng categories
CREATE TABLE categories (
                            category_id SERIAL PRIMARY KEY,
                            name VARCHAR(100) NOT NULL,
                            description TEXT
);

-- 3️⃣ Bảng products
CREATE TABLE products (
                          product_id SERIAL PRIMARY KEY,
                          name VARCHAR(150) NOT NULL,
                          description TEXT,
                          price NUMERIC(12, 2) NOT NULL CHECK (price >= 0),
                          stock INT DEFAULT 0 CHECK (stock >= 0),
                          category_id INT REFERENCES categories(category_id) ON DELETE SET NULL,
                          created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 4️⃣ Bảng orders
CREATE TABLE orders (
                        order_id SERIAL PRIMARY KEY,
                        user_id INT REFERENCES users(user_id) ON DELETE CASCADE,
                        order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        total_amount NUMERIC(12, 2) DEFAULT 0 CHECK (total_amount >= 0)
);

-- 5️⃣ Bảng order_details (trung gian orders ↔ products)
CREATE TABLE order_details (
                               order_id INT REFERENCES orders(order_id) ON DELETE CASCADE,
                               product_id INT REFERENCES products(product_id) ON DELETE CASCADE,
                               quantity INT NOT NULL CHECK (quantity > 0),
                               price NUMERIC(12, 2) NOT NULL CHECK (price >= 0),
                               PRIMARY KEY (order_id, product_id)
);

-- 6️⃣ Bảng reviews (trung gian users ↔ products)
CREATE TABLE reviews (
                         review_id SERIAL PRIMARY KEY,
                         user_id INT REFERENCES users(user_id) ON DELETE CASCADE,
                         product_id INT REFERENCES products(product_id) ON DELETE CASCADE,
                         rate INT CHECK (rate BETWEEN 1 AND 5),
                         comment TEXT,
                         created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
-- === 1️⃣ USERS ===
INSERT INTO users (username, email, password_hash)
VALUES
    ('alice', 'alice@example.com', 'hash_pwd_1'),
    ('bob', 'bob@example.com', 'hash_pwd_2'),
    ('charlie', 'charlie@example.com', 'hash_pwd_3');

-- === 2️⃣ CATEGORIES ===
INSERT INTO categories (name, description)
VALUES
    ('Electronics', 'Electronic gadgets and devices'),
    ('Clothing', 'Men and women apparel'),
    ('Books', 'Various genres of books');

-- === 3️⃣ PRODUCTS ===
INSERT INTO products (name, description, price, stock, category_id)
VALUES
    ('Smartphone', 'Latest 5G smartphone with OLED display', 699.99, 50, 1),
    ('Laptop', 'Lightweight laptop with 16GB RAM', 1199.00, 20, 1),
    ('T-shirt', 'Cotton t-shirt in various sizes', 19.99, 100, 2),
    ('Novel: The Great Adventure', 'A best-selling adventure novel', 12.50, 40, 3);

-- === 4️⃣ ORDERS ===
INSERT INTO orders (user_id, order_date, total_amount)
VALUES
    (1, NOW(), 719.98),  -- Alice
    (2, NOW(), 1231.50), -- Bob
    (3, NOW(), 19.99);   -- Charlie

-- === 5️⃣ ORDER_DETAILS ===
INSERT INTO order_details (order_id, product_id, quantity, price)
VALUES
    (1, 1, 1, 699.99),   -- Alice buys Smartphone
    (1, 3, 1, 19.99),    -- Alice buys T-shirt
    (2, 2, 1, 1199.00),  -- Bob buys Laptop
    (2, 4, 1, 12.50),    -- Bob buys a book
    (3, 3, 1, 19.99);    -- Charlie buys a T-shirt

-- === 6️⃣ REVIEWS ===
INSERT INTO reviews (user_id, product_id, rate, comment)
VALUES
    (1, 1, 5, 'Excellent phone, very smooth performance!'),
    (1, 3, 4, 'Good quality shirt but size runs small.'),
    (2, 2, 5, 'Laptop works perfectly for my workflow.'),
    (3, 4, 3, 'The story was okay but predictable.');

CREATE DATABASE employeesdatabase;
\connect employeesdatabase

CREATE TABLE departments (
    id SERIAL PRIMARY KEY,
    phone TEXT UNIQUE NOT NULL,
    name TEXT UNIQUE NOT NULL,
);

CREATE TABLE employees (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    surname TEXT NOT NULL,
    phone TEXT NOT NULL,
    company_id INTEGER NOT NULL,
	department_id INTEGER NOT NULL,
	UNIQUE(phone)
);

CREATE TABLE passports (
    type TEXT NOT NULL,
	number TEXT NOT NULL,
	employee_id INTEGER NOT NULL,
	PRIMARY KEY (type, number),
    UNIQUE(employee_id)
);

ALTER TABLE passports
ADD FOREIGN KEY (employee_id) 
REFERENCES employees (id) ON DELETE CASCADE;

ALTER TABLE employees
ADD FOREIGN KEY (department_id) 
REFERENCES departments (id) ON DELETE CASCADE;

INSERT INTO departments (Phone, Name) 
VALUES  ('1', 'Отдел 1'),
        ('2', 'Отдел 2');

INSERT INTO employees (name, surname, phone, company_id, department_id)
VALUES  ('Раиль', 'Тинчурин', '111-111', 1, 1),
        ('Робинзон', 'Бастард', '222-222', 2, 2);


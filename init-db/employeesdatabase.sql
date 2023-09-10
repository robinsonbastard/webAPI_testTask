CREATE DATABASE employeesdatabase;
\connect employeesdatabase

CREATE TABLE departments (
    Id SERIAL PRIMARY KEY,
    Phone TEXT NOT NULL,
    Name TEXT NOT NULL,
    UNIQUE (Phone, Name)
);

CREATE TABLE employees
(
    Id SERIAL PRIMARY KEY,
    Name TEXT NOT NULL,
    Surname TEXT NOT NULL,
    Phone TEXT NOT NULL,
    CompanyId INTEGER NOT NULL,
	PassportType TEXT NOT NULL,
	PassportNumber TEXT NOT NULL,
	DepartmentId INTEGER NOT NULL,
	UNIQUE(Phone, PassportNumber, PassportType)
);

ALTER TABLE employees
ADD FOREIGN KEY (DepartmentId) REFERENCES departments (Id);

INSERT INTO departments (Phone, Name) 
VALUES  ('1', 'Отдел 1'),
        ('2', 'Отдел 2'),
        ('3', 'Отдел 3'),
        ('4', 'Отдел 4'),
        ('5', 'Отдел 5');

INSERT INTO employees (name, surname, phone, companyid, departmentid, passporttype, passportnumber)
VALUES  ('Раиль', 'Тинчурин', '111-111', '1', '1', '1', '1111'),
        ('Робинзон', 'Бастард', '222-222', '2', '2', '2', '2222');


CREATE TABLE IF NOT EXISTS QuestionSet (
    QuestionID INTEGER PRIMARY KEY, 
    Type TEXT NOT NULL,       
    Difficulty INTEGER NOT NULL,
    Seen boolean NOT NULL              
);

CREATE TABLE IF NOT EXISTS ProblemSet (
    ProblemID INTEGER PRIMARY KEY, 
    Whole INTEGER DEFAULT 0,
    Half INTEGER DEFAULT 0,
    Third INTEGER DEFAULT 0, 
    Forth INTEGER DEFAULT 0,
    Fifth INTEGER DEFAULT 0   
);

CREATE TABLE IF NOT EXISTS SolutionSet (
    SolutionID INTEGER PRIMARY KEY, 
    Whole INTEGER DEFAULT 0,
    Half INTEGER DEFAULT 0,
    Third INTEGER DEFAULT 0, 
    Forth INTEGER DEFAULT 0,
    Fifth INTEGER DEFAULT 0                       
);

INSERT INTO QuestionSet (Type, Difficulty, Seen)
VALUES ('Fraction', 1, False),
       ('Fraction', 1, False),
       ('Fraction', 2, False);

INSERT INTO ProblemSet (Whole, Half, Third, Forth, Fifth)
VALUES (3, 0, 0, 0, 0),
       (0, 1, 0, 0, 0),
       (0, 0, 1, 2, 0);

INSERT INTO SolutionSet (Whole, Half, Third, Forth, Fifth)
VALUES (4, 1, 1, 0, 0),
       (1, 1, 1, 1, 1),
       (1, 1, 1, 0, 0);


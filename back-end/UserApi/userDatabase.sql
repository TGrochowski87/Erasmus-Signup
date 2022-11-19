CREATE EXTENSION citext;

CREATE TYPE t_pwr_fac_sh AS ENUM (
    'PWR', 'W1', 'W2', 'W3', 'W4N', 'W5', 'W6', 'W7', 'W8N', 'W9', 'W10', 'W11', 'W12N', 'W13', 'W15', 'F3'
);

-- jestesmy pewni ze to nie bedzie po prostu enum?
CREATE TABLE role (
    id                      smallserial PRIMARY KEY,
    name                    varchar(50) NOT NULL
);

CREATE TABLE app_user (
    id                      serial PRIMARY KEY,
    first_name              varchar(50) NOT NULL,
    last_name               varchar(50) NOT NULL,
    role_id                 smallint REFERENCES role,
    email                   citext NOT NULL,
    faculty_id              t_pwr_fac_sh NOT NULL
);

CREATE TABLE nofitifaction (
    id                      serial PRIMARY KEY,
    user_id                 integer REFERENCES app_user,
    content                 text NOT NULL,
    url                     varchar(50) NOT NULL
);

CREATE TABLE destination_list_preference (
    id                      serial PRIMARY KEY,
    name                    varchar(50) NOT NULL
);

-- znowu imo pasowalby tu enum
CREATE TABLE destination_list_type (
    id                      serial PRIMARY KEY,
    name                    varchar(50) NOT NULL
);

CREATE TABLE destination_list (
    id                      serial PRIMARY KEY,
    user_id                 integer REFERENCES app_user,
    preference_list_id      integer REFERENCES destination_list_preference,
    list_type_id            integer REFERENCES destination_list_type,
    destination_id          smallint NOT NULL
);

CREATE TABLE coordinator (
    user_id                 integer REFERENCES app_user,
    pwr_speciality          integer NOT NULL,
    PRIMARY KEY (user_id)
);

CREATE TABLE student (
    user_id                 integer REFERENCES app_user,
    index                   varchar(50) NOT NULL,
    pwr_speciality          integer NOT NULL,
    average_grade           real NOT NULL,
    semester                smallint NOT NULL,
    PRIMARY KEY (user_id)
);

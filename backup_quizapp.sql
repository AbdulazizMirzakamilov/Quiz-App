--
-- PostgreSQL database dump
--

\restrict 4UOqjEKkx4khe98cL4dAC8lw5Z038CnjjwQ9otKvYxkeiNoX1Jpra7GaCk3prAC

-- Dumped from database version 18.1
-- Dumped by pg_dump version 18.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: AnswerOption; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AnswerOption" (
    answerid integer CONSTRAINT answeroption_answerid_not_null NOT NULL,
    questionid integer CONSTRAINT answeroption_questionid_not_null NOT NULL,
    answertext text CONSTRAINT answeroption_answertext_not_null NOT NULL,
    iscorrect boolean DEFAULT false CONSTRAINT answeroption_iscorrect_not_null NOT NULL
);


ALTER TABLE public."AnswerOption" OWNER TO postgres;

--
-- Name: AttemptAnswer; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AttemptAnswer" (
    attemptanswerid integer CONSTRAINT attemptanswer_attemptanswerid_not_null NOT NULL,
    attemptid integer CONSTRAINT attemptanswer_attemptid_not_null NOT NULL,
    questionid integer CONSTRAINT attemptanswer_questionid_not_null NOT NULL,
    answerid integer CONSTRAINT attemptanswer_answerid_not_null NOT NULL,
    iscorrect boolean CONSTRAINT attemptanswer_iscorrect_not_null NOT NULL
);


ALTER TABLE public."AttemptAnswer" OWNER TO postgres;

--
-- Name: Question; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Question" (
    questionid integer CONSTRAINT question_questionid_not_null NOT NULL,
    quizid integer CONSTRAINT question_quizid_not_null NOT NULL,
    questiontext text CONSTRAINT question_questiontext_not_null NOT NULL
);


ALTER TABLE public."Question" OWNER TO postgres;

--
-- Name: Quiz; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Quiz" (
    quizid integer CONSTRAINT quiz_quizid_not_null NOT NULL,
    title character varying(200) CONSTRAINT quiz_title_not_null NOT NULL,
    description text,
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public."Quiz" OWNER TO postgres;

--
-- Name: QuizAttempt; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."QuizAttempt" (
    attemptid integer CONSTRAINT quizattempt_attemptid_not_null NOT NULL,
    quizid integer CONSTRAINT quizattempt_quizid_not_null NOT NULL,
    username character varying(100),
    startedat timestamp without time zone DEFAULT now(),
    score integer DEFAULT 0,
    totalquestions integer
);


ALTER TABLE public."QuizAttempt" OWNER TO postgres;

--
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    userid integer NOT NULL,
    username character varying(50) NOT NULL,
    email character varying(100) NOT NULL,
    passwordhash text NOT NULL,
    createdat timestamp without time zone DEFAULT now(),
    isadmin boolean DEFAULT false
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- Name: Users_userid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Users_userid_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Users_userid_seq" OWNER TO postgres;

--
-- Name: Users_userid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Users_userid_seq" OWNED BY public."Users".userid;


--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Name: answeroption_answerid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.answeroption_answerid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.answeroption_answerid_seq OWNER TO postgres;

--
-- Name: answeroption_answerid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.answeroption_answerid_seq OWNED BY public."AnswerOption".answerid;


--
-- Name: attemptanswer_attemptanswerid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.attemptanswer_attemptanswerid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.attemptanswer_attemptanswerid_seq OWNER TO postgres;

--
-- Name: attemptanswer_attemptanswerid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.attemptanswer_attemptanswerid_seq OWNED BY public."AttemptAnswer".attemptanswerid;


--
-- Name: question_questionid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.question_questionid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.question_questionid_seq OWNER TO postgres;

--
-- Name: question_questionid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.question_questionid_seq OWNED BY public."Question".questionid;


--
-- Name: quiz_quizid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.quiz_quizid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.quiz_quizid_seq OWNER TO postgres;

--
-- Name: quiz_quizid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.quiz_quizid_seq OWNED BY public."Quiz".quizid;


--
-- Name: quizattempt_attemptid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.quizattempt_attemptid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.quizattempt_attemptid_seq OWNER TO postgres;

--
-- Name: quizattempt_attemptid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.quizattempt_attemptid_seq OWNED BY public."QuizAttempt".attemptid;


--
-- Name: AnswerOption answerid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnswerOption" ALTER COLUMN answerid SET DEFAULT nextval('public.answeroption_answerid_seq'::regclass);


--
-- Name: AttemptAnswer attemptanswerid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AttemptAnswer" ALTER COLUMN attemptanswerid SET DEFAULT nextval('public.attemptanswer_attemptanswerid_seq'::regclass);


--
-- Name: Question questionid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Question" ALTER COLUMN questionid SET DEFAULT nextval('public.question_questionid_seq'::regclass);


--
-- Name: Quiz quizid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Quiz" ALTER COLUMN quizid SET DEFAULT nextval('public.quiz_quizid_seq'::regclass);


--
-- Name: QuizAttempt attemptid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."QuizAttempt" ALTER COLUMN attemptid SET DEFAULT nextval('public.quizattempt_attemptid_seq'::regclass);


--
-- Name: Users userid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users" ALTER COLUMN userid SET DEFAULT nextval('public."Users_userid_seq"'::regclass);


--
-- Data for Name: AnswerOption; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AnswerOption" (answerid, questionid, answertext, iscorrect) FROM stdin;
1	1	Сокрытие реализации	t
2	1	Наследование классов	f
3	1	Множественная сборка	f
4	2	int	t
5	2	string	f
6	2	bool	f
7	3	new	t
8	3	class	f
9	3	create	f
10	4	Для импорта пространств имен	t
11	4	Для наследования классов	f
12	4	Для логирования	f
13	5	Конструктор	t
14	5	Деструктор	f
15	5	Геттер	f
16	6	Париж	t
17	6	Лион	f
18	6	Марсель	f
19	7	Россия	t
20	7	Китай	f
21	7	Канада	f
22	8	Токио	t
23	8	Осака	f
24	8	Киото	f
25	9	Южная Америка	t
26	9	Африка	f
27	9	Европа	f
28	10	Нил	t
29	10	Амазонка	f
30	10	Янцзы	f
31	11	56	t
32	11	54	f
33	11	64	f
34	12	9	t
35	12	8	f
36	12	7	f
37	13	90	t
38	13	180	f
39	13	45	f
40	14	42	t
41	14	40	f
42	14	39	f
43	15	25	t
44	15	20	f
45	15	30	f
\.


--
-- Data for Name: AttemptAnswer; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AttemptAnswer" (attemptanswerid, attemptid, questionid, answerid, iscorrect) FROM stdin;
1	1	1	1	t
2	1	2	4	t
3	1	3	7	t
4	1	4	10	t
5	1	5	13	f
6	2	6	16	t
7	2	7	19	t
8	2	8	22	f
9	2	9	25	t
10	2	10	28	f
\.


--
-- Data for Name: Question; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Question" (questionid, quizid, questiontext) FROM stdin;
1	1	Что такое инкапсуляция в ООП?
2	1	Какой тип данных хранит целые числа?
3	1	Ключевое слово для создания объекта?
4	1	Где используется using?
5	1	Как называется метод, который запускается при создании объекта?
6	2	Столица Франции?
7	2	Какая страна самая большая по площади?
8	2	Столица Японии?
9	2	На каком континенте находится Бразилия?
10	2	Самая длинная река мира?
11	3	Сколько будет 7 * 8?
12	3	Чему равно √81?
13	3	Сколько градусов в прямом угле?
14	3	Сколько будет 15 + 27?
15	3	Чему равно 100 / 4?
\.


--
-- Data for Name: Quiz; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Quiz" (quizid, title, description, createdat) FROM stdin;
1	Основы C#	Викторина по базовым концепциям C#	2025-12-02 13:17:10.320119
2	География мира	Проверь знания стран и столиц	2025-12-02 13:17:10.320119
3	Математические задачки	Простые вопросы по математике	2025-12-02 13:17:10.320119
\.


--
-- Data for Name: QuizAttempt; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."QuizAttempt" (attemptid, quizid, username, startedat, score, totalquestions) FROM stdin;
1	1	Abdulaziz	2025-12-02 13:20:54.787795	4	5
2	2	Sanya	2025-12-02 13:20:54.787795	3	5
3	2	Игрок (WPF)	2025-12-06 01:12:52.373032	3	5
4	2	Игрок (WPF)	2025-12-06 01:39:05.170316	5	5
6	3	Игрок (WPF)	2025-12-06 12:36:17.59396	5	5
7	3	Игрок (WPF)	2025-12-06 20:18:46.900768	5	5
8	1	Игрок (WPF)	2025-12-12 21:59:16.36803	1	5
12	3	Игрок (WPF)	2025-12-13 14:59:02.268424	5	5
13	1	Игрок (WPF)	2025-12-15 21:09:17.175897	4	5
\.


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Users" (userid, username, email, passwordhash, createdat, isadmin) FROM stdin;
1	Aziz	alikom@gmail.com	FEC59172F412AC871B74FE0EDE718FF7536D596AF6165D78F80E3A10580C13F4	2025-12-13 00:00:08.811708	t
2	admin	admin@examle.com	5994471ABB01112AFCC18159F6CC74B4F511B99806DA59B3CAF5A9C173CACFC5	2025-12-15 20:24:10.814579	t
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
\.


--
-- Name: Users_userid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Users_userid_seq"', 2, true);


--
-- Name: answeroption_answerid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.answeroption_answerid_seq', 73, true);


--
-- Name: attemptanswer_attemptanswerid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.attemptanswer_attemptanswerid_seq', 10, true);


--
-- Name: question_questionid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.question_questionid_seq', 22, true);


--
-- Name: quiz_quizid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.quiz_quizid_seq', 7, true);


--
-- Name: quizattempt_attemptid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.quizattempt_attemptid_seq', 13, true);


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: Users Users_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_email_key" UNIQUE (email);


--
-- Name: Users Users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY (userid);


--
-- Name: Users Users_username_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_username_key" UNIQUE (username);


--
-- Name: AnswerOption answeroption_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnswerOption"
    ADD CONSTRAINT answeroption_pkey PRIMARY KEY (answerid);


--
-- Name: AttemptAnswer attemptanswer_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AttemptAnswer"
    ADD CONSTRAINT attemptanswer_pkey PRIMARY KEY (attemptanswerid);


--
-- Name: Question question_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Question"
    ADD CONSTRAINT question_pkey PRIMARY KEY (questionid);


--
-- Name: Quiz quiz_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Quiz"
    ADD CONSTRAINT quiz_pkey PRIMARY KEY (quizid);


--
-- Name: QuizAttempt quizattempt_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."QuizAttempt"
    ADD CONSTRAINT quizattempt_pkey PRIMARY KEY (attemptid);


--
-- Name: AnswerOption fk_answeroption_question; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AnswerOption"
    ADD CONSTRAINT fk_answeroption_question FOREIGN KEY (questionid) REFERENCES public."Question"(questionid) ON DELETE CASCADE;


--
-- Name: AttemptAnswer fk_attemptanswer_answer; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AttemptAnswer"
    ADD CONSTRAINT fk_attemptanswer_answer FOREIGN KEY (answerid) REFERENCES public."AnswerOption"(answerid) ON DELETE CASCADE;


--
-- Name: AttemptAnswer fk_attemptanswer_attempt; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AttemptAnswer"
    ADD CONSTRAINT fk_attemptanswer_attempt FOREIGN KEY (attemptid) REFERENCES public."QuizAttempt"(attemptid) ON DELETE CASCADE;


--
-- Name: AttemptAnswer fk_attemptanswer_question; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AttemptAnswer"
    ADD CONSTRAINT fk_attemptanswer_question FOREIGN KEY (questionid) REFERENCES public."Question"(questionid) ON DELETE CASCADE;


--
-- Name: Question fk_question_quiz; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Question"
    ADD CONSTRAINT fk_question_quiz FOREIGN KEY (quizid) REFERENCES public."Quiz"(quizid) ON DELETE CASCADE;


--
-- Name: QuizAttempt fk_quizattempt_quiz; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."QuizAttempt"
    ADD CONSTRAINT fk_quizattempt_quiz FOREIGN KEY (quizid) REFERENCES public."Quiz"(quizid) ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict 4UOqjEKkx4khe98cL4dAC8lw5Z038CnjjwQ9otKvYxkeiNoX1Jpra7GaCk3prAC


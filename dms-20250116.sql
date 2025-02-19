PGDMP  )                     }            dbsiram    17.0    17.0 C    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    16479    dbsiram    DATABASE     �   CREATE DATABASE dbsiram WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
    DROP DATABASE dbsiram;
                     postgres    false                        2615    16481    dms    SCHEMA        CREATE SCHEMA dms;
    DROP SCHEMA dms;
                     postgres    false                       1255    16484    fc_dir_tree(integer, integer)    FUNCTION     �	  CREATE FUNCTION dms.fc_dir_tree(dir_id integer, selected_id integer) RETURNS TABLE(id text, parent text, text text, icon text, state text, opened boolean, disabled boolean, selected boolean, li_attr text, a_attr text, level integer, tree character varying)
    LANGUAGE plpgsql
    AS $$


    BEGIN
    return query 
       WITH RECURSIVE subordinates AS (
            SELECT
                d1.id,
                d1.parent_id as "parent",
                d1.directory_name as "text",
                'fa fa-folder-open text-warning' as "icon",
                null as "state",
                false as "opened",
                false as "disabled",
                false as "selected",
                null as "li_attr",
                null as "a_attr",
                0 AS "level"
            ,d1.id::VARCHAR as "tree"
            FROM
                dms."directories" d1
            WHERE d1.id = "dir_id" --AND d1.parent_id != 0
            UNION
                SELECT
                    e.id,
                    e.parent_id as "parent",
                    e.directory_name as "text",
                    'fa fa-folder-open text-warning' as "icon",
                    null as "state",
                    false as "opened",
                    false as "disabled",
                    false as "selected",
                    null as "li_attr",
                    null as "a_attr",
                    s."level" + 1
                    ,s."tree"::VARCHAR || ',' || e.id::VARCHAR
                FROM
                    dms."directories" e
                INNER JOIN subordinates s ON s.id = e.parent_id
        ) SELECT
            subordinates.id::text,
            
             case 
                when subordinates.parent = 0 then '#' 
                else subordinates.parent::text
              end
              ,
              
              subordinates.text,
            subordinates.icon,
            subordinates.state,
            case 
                when subordinates.id = "selected_id" then true
                else false
            end,
            subordinates.disabled,
            case 
                when subordinates.id = "selected_id" then true
                else false
            end,
            subordinates.li_attr,
            subordinates.a_attr,
            subordinates.level,
            subordinates.tree
        FROM subordinates ORDER BY subordinates."tree" asc;
    END
$$;
 D   DROP FUNCTION dms.fc_dir_tree(dir_id integer, selected_id integer);
       dms               postgres    false    6                       1255    16485    fc_dt(integer)    FUNCTION     z  CREATE FUNCTION dms.fc_dt(cid integer) RETURNS TABLE(id integer, cdt_id integer, collection_id integer, name text, description text, selected text)
    LANGUAGE plpgsql
    AS $$
   
BEGIN
    return query
    SELECT dt.id,cdt.id cdt_id,cdt.collection_id, dt.name, dt.description,
    CASE WHEN dt.id = cdt.document_type_id THEN 'selected'
    ELSE ''
    END as selected
    FROM public.document_types dt
    LEFT JOIN (SELECT xx.id, xx.collection_id, xx.document_type_id FROM public.collection_document_types xx WHERE xx.collection_id=cid) as cdt 
    ON dt.id=cdt.document_type_id 
    ORDER BY dt.id asc;
    END
$$;
 &   DROP FUNCTION dms.fc_dt(cid integer);
       dms               postgres    false    6            �            1259    16486    __EFMigrationsHistory    TABLE     �   CREATE TABLE dms."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 (   DROP TABLE dms."__EFMigrationsHistory";
       dms         heap r       postgres    false    6            �            1259    16489    collection_document_types    TABLE     �  CREATE TABLE dms.collection_document_types (
    id integer NOT NULL,
    collection_id character varying(50),
    document_type_id character varying(50),
    status integer NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by character varying(10),
    updated_at timestamp without time zone,
    last_updated_at timestamp without time zone,
    updated_by character varying(10)
);
 *   DROP TABLE dms.collection_document_types;
       dms         heap r       postgres    false    6            �            1259    16492     collection_document_types_id_seq    SEQUENCE     �   ALTER TABLE dms.collection_document_types ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME dms.collection_document_types_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            dms               postgres    false    222    6            �            1259    16493    collections    TABLE     �  CREATE TABLE dms.collections (
    id integer NOT NULL,
    collection_id character varying(50),
    collection_name text,
    description text,
    status integer NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by character varying(10),
    updated_at timestamp without time zone,
    last_updated_at timestamp without time zone,
    updated_by character varying(10)
);
    DROP TABLE dms.collections;
       dms         heap r       postgres    false    6            �            1259    16498    collections_id_seq    SEQUENCE     �   ALTER TABLE dms.collections ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME dms.collections_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            dms               postgres    false    224    6            �            1259    16499    directories    TABLE       CREATE TABLE dms.directories (
    id integer NOT NULL,
    directory_id character varying(50),
    directory_name text,
    disk text,
    path_name text,
    parent_id integer NOT NULL,
    owner_id character varying(10),
    status integer NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by character varying(10),
    updated_at timestamp without time zone,
    last_updated_at timestamp without time zone,
    updated_by character varying(10),
    collection_id character varying(50)
);
    DROP TABLE dms.directories;
       dms         heap r       postgres    false    6            �            1259    16504    directories_id_seq    SEQUENCE     �   ALTER TABLE dms.directories ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME dms.directories_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            dms               postgres    false    6    226                       1259    17416    document_delete    TABLE     ^  CREATE TABLE dms.document_delete (
    document_delete_id character varying(50) NOT NULL,
    document_id character varying(50),
    user_id character varying(50),
    created_at timestamp without time zone,
    created_by character varying(50),
    updated_at timestamp without time zone,
    updated_by character varying(50),
    status integer
);
     DROP TABLE dms.document_delete;
       dms         heap r       postgres    false    6            �            1259    16505    document_favorites    TABLE     �  CREATE TABLE dms.document_favorites (
    id integer NOT NULL,
    document_id character varying(50),
    user_id character varying(50),
    status integer NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by character varying(10),
    updated_at timestamp without time zone,
    last_updated_at timestamp without time zone,
    updated_by character varying(10)
);
 #   DROP TABLE dms.document_favorites;
       dms         heap r       postgres    false    6            �            1259    16508    document_favorites_id_seq    SEQUENCE     �   ALTER TABLE dms.document_favorites ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME dms.document_favorites_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            dms               postgres    false    6    228            �            1259    16509    document_indices    TABLE     �  CREATE TABLE dms.document_indices (
    id integer NOT NULL,
    index_name character varying(50),
    rules text,
    index_value text,
    document_type_id character varying(50) NOT NULL,
    status integer NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by character varying(10),
    updated_at timestamp without time zone,
    last_updated_at timestamp without time zone,
    updated_by character varying(10),
    index_id character varying(50)
);
 !   DROP TABLE dms.document_indices;
       dms         heap r       postgres    false    6            �            1259    16514    document_indices_id_seq    SEQUENCE     �   ALTER TABLE dms.document_indices ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME dms.document_indices_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            dms               postgres    false    6    230                       1259    17060    document_indices_value    TABLE     �  CREATE TABLE dms.document_indices_value (
    id integer NOT NULL,
    document_type_id character varying(50),
    index_id character varying(50),
    index_value character varying(150),
    created_at timestamp without time zone,
    created_by character varying(20),
    last_updated_at timestamp without time zone,
    updated_at timestamp without time zone,
    updated_by character varying(20),
    document_id character varying(50)
);
 '   DROP TABLE dms.document_indices_value;
       dms         heap r       postgres    false    6                       1259    17338    document_tags    TABLE     >  CREATE TABLE dms.document_tags (
    id integer NOT NULL,
    user_id character varying(50),
    document_id character varying(50),
    created_by character varying(50),
    created_at timestamp without time zone,
    updated_by character varying(50),
    updated_at timestamp without time zone,
    tags_json text
);
    DROP TABLE dms.document_tags;
       dms         heap r       postgres    false    6                       1259    17341    document_tags_id_seq    SEQUENCE     �   ALTER TABLE dms.document_tags ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME dms.document_tags_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            dms               postgres    false    6    278                       1259    17411    document_tags_value    TABLE     ~  CREATE TABLE dms.document_tags_value (
    document_id character varying(50),
    document_tag_name character varying(50),
    created_by character varying(50),
    created_at timestamp without time zone,
    updated_by character varying(50),
    updated_at timestamp without time zone,
    user_id character varying(50),
    document_tag_value_id character varying(50) NOT NULL
);
 $   DROP TABLE dms.document_tags_value;
       dms         heap r       postgres    false    6            �            1259    16515    document_types    TABLE     �  CREATE TABLE dms.document_types (
    id integer NOT NULL,
    document_type_id character varying(50),
    document_type_name text,
    description text,
    status integer NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by character varying(10),
    updated_at timestamp without time zone,
    last_updated_at timestamp without time zone,
    updated_by character varying(10)
);
    DROP TABLE dms.document_types;
       dms         heap r       postgres    false    6            �            1259    16520    document_types_id_seq    SEQUENCE     �   ALTER TABLE dms.document_types ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME dms.document_types_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            dms               postgres    false    6    232                       1259    17446    document_versions    TABLE     J  CREATE TABLE dms.document_versions (
    id integer NOT NULL,
    name character varying(150),
    description character varying(255),
    version_number integer,
    file_path character varying(255),
    document_id character varying(50),
    user_id character varying(50),
    created_at timestamp without time zone,
    created_by character varying(50),
    updated_at timestamp without time zone,
    updated_by character varying(50),
    expired_date date,
    file_size bigint,
    extension character varying(10),
    content_type character varying(50),
    encrypt_file text
);
 "   DROP TABLE dms.document_versions;
       dms         heap r       postgres    false    6                       1259    17445    document_versions_id_seq    SEQUENCE     �   ALTER TABLE dms.document_versions ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME dms.document_versions_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            dms               postgres    false    6    284            �            1259    16521 	   documents    TABLE       CREATE TABLE dms.documents (
    id integer NOT NULL,
    document_id character varying(50),
    file_name text,
    content_type character varying(255),
    extension character varying(10),
    file_path text,
    collection_id character varying(50),
    document_type_id character varying(50),
    directory_id character varying(50),
    version integer NOT NULL,
    file_size bigint NOT NULL,
    download integer NOT NULL,
    owner_id character varying(10),
    expired_date timestamp without time zone,
    status integer NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by character varying(10),
    updated_at timestamp without time zone,
    last_updated_at timestamp without time zone,
    updated_by character varying(10),
    document_name character varying(50),
    reference character varying(50),
    date_version date,
    document_no character varying(50),
    encrypt_file text,
    download_date timestamp without time zone,
    delete_date timestamp without time zone,
    delete boolean
);
    DROP TABLE dms.documents;
       dms         heap r       postgres    false    6            �            1259    16526    documents_id_seq    SEQUENCE     �   ALTER TABLE dms.documents ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME dms.documents_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            dms               postgres    false    6    234                       1259    17059    newtable_id_seq    SEQUENCE     �   ALTER TABLE dms.document_indices_value ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME dms.newtable_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            dms               postgres    false    6    277                       1259    17423 
   v_document    VIEW     �  CREATE VIEW dms.v_document AS
 SELECT d.id,
    d.document_id,
    d.file_name,
    d.content_type,
    d.extension,
    d.file_path,
    d.collection_id,
    d.document_type_id,
    d2.id AS id_directory,
    d.directory_id,
    d.version,
    d.file_size,
    d.download,
    d.owner_id,
    d.expired_date,
    d.status,
    d.created_at,
    d.created_by,
    d.updated_at,
    d.last_updated_at,
    d.updated_by,
    d.document_no,
    d.document_name,
    d.reference,
    d.date_version,
    d.encrypt_file,
    d.download_date,
    df.user_id,
        CASE
            WHEN (df.total_favorite > 0) THEN true
            ELSE false
        END AS favorite,
    dt.id AS id_tags,
    dt.tags_json
   FROM (((dms.documents d
     JOIN ( SELECT df_1.document_id,
            count(df_1.document_id) AS total_favorite,
            df_1.user_id,
            df_1.updated_at
           FROM dms.document_favorites df_1
          WHERE (df_1.status = 1)
          GROUP BY df_1.document_id, df_1.user_id, df_1.updated_at) df ON (((d.document_id)::text = (df.document_id)::text)))
     LEFT JOIN dms.directories d2 ON (((d.directory_id)::text = (d2.directory_id)::text)))
     LEFT JOIN dms.document_tags dt ON (((dt.document_id)::text = (d.document_id)::text)))
UNION ALL
 SELECT d.id,
    d.document_id,
    d.file_name,
    d.content_type,
    d.extension,
    d.file_path,
    d.collection_id,
    d.document_type_id,
    d2.id AS id_directory,
    d.directory_id,
    d.version,
    d.file_size,
    d.download,
    d.owner_id,
    d.expired_date,
    d.status,
    d.created_at,
    d.created_by,
    d.updated_at,
    d.last_updated_at,
    d.updated_by,
    d.document_no,
    d.document_name,
    d.reference,
    d.date_version,
    d.encrypt_file,
    d.download_date,
    d.created_by AS user_id,
    false AS favorite,
    dt.id AS id_tags,
    dt.tags_json
   FROM ((dms.documents d
     LEFT JOIN dms.directories d2 ON (((d.directory_id)::text = (d2.directory_id)::text)))
     LEFT JOIN dms.document_tags dt ON (((dt.document_id)::text = (d.document_id)::text)))
  WHERE (NOT ((d.document_id)::text IN ( SELECT df.document_id
           FROM dms.document_favorites df
          WHERE (df.status = 1))));
    DROP VIEW dms.v_document;
       dms       v       postgres    false    234    278    278    278    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    234    228    228    228    228    226    226    6                       1259    17534    v_document_versions    VIEW     N  CREATE VIEW dms.v_document_versions AS
 SELECT dv.id,
    d.document_id,
    d.document_name,
    dv.version_number,
    dv.name,
    dv.file_size,
    dv.file_path,
    dv.extension,
    dv.user_id,
    dv.created_at
   FROM (dms.documents d
     JOIN dms.document_versions dv ON (((d.document_id)::text = (dv.document_id)::text)));
 #   DROP VIEW dms.v_document_versions;
       dms       v       postgres    false    284    284    284    284    284    284    284    284    234    234    284    6            �            1259    16527    v_root    VIEW     ~  CREATE VIEW dms.v_root AS
 SELECT (id)::text AS id,
    '#'::text AS parent,
    directory_name AS text,
    'fa fa-folder-open text-warning'::text AS icon,
    NULL::text AS state,
    false AS opened,
    false AS disabled,
    false AS selected,
    NULL::text AS li_attr,
    NULL::text AS a_attr,
    0 AS level,
    (id)::character varying AS tree
   FROM dms.directories d1;
    DROP VIEW dms.v_root;
       dms       v       postgres    false    226    226    6            �          0    16486    __EFMigrationsHistory 
   TABLE DATA           O   COPY dms."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    dms               postgres    false    221   �w       �          0    16489    collection_document_types 
   TABLE DATA           �   COPY dms.collection_document_types (id, collection_id, document_type_id, status, created_at, created_by, updated_at, last_updated_at, updated_by) FROM stdin;
    dms               postgres    false    222   x       �          0    16493    collections 
   TABLE DATA           �   COPY dms.collections (id, collection_id, collection_name, description, status, created_at, created_by, updated_at, last_updated_at, updated_by) FROM stdin;
    dms               postgres    false    224   +x       �          0    16499    directories 
   TABLE DATA           �   COPY dms.directories (id, directory_id, directory_name, disk, path_name, parent_id, owner_id, status, created_at, created_by, updated_at, last_updated_at, updated_by, collection_id) FROM stdin;
    dms               postgres    false    226   �x       �          0    17416    document_delete 
   TABLE DATA           �   COPY dms.document_delete (document_delete_id, document_id, user_id, created_at, created_by, updated_at, updated_by, status) FROM stdin;
    dms               postgres    false    281   2y       �          0    16505    document_favorites 
   TABLE DATA           �   COPY dms.document_favorites (id, document_id, user_id, status, created_at, created_by, updated_at, last_updated_at, updated_by) FROM stdin;
    dms               postgres    false    228   Oy       �          0    16509    document_indices 
   TABLE DATA           �   COPY dms.document_indices (id, index_name, rules, index_value, document_type_id, status, created_at, created_by, updated_at, last_updated_at, updated_by, index_id) FROM stdin;
    dms               postgres    false    230   ly       �          0    17060    document_indices_value 
   TABLE DATA           �   COPY dms.document_indices_value (id, document_type_id, index_id, index_value, created_at, created_by, last_updated_at, updated_at, updated_by, document_id) FROM stdin;
    dms               postgres    false    277   cz       �          0    17338    document_tags 
   TABLE DATA           y   COPY dms.document_tags (id, user_id, document_id, created_by, created_at, updated_by, updated_at, tags_json) FROM stdin;
    dms               postgres    false    278   K{       �          0    17411    document_tags_value 
   TABLE DATA           �   COPY dms.document_tags_value (document_id, document_tag_name, created_by, created_at, updated_by, updated_at, user_id, document_tag_value_id) FROM stdin;
    dms               postgres    false    280   |       �          0    16515    document_types 
   TABLE DATA           �   COPY dms.document_types (id, document_type_id, document_type_name, description, status, created_at, created_by, updated_at, last_updated_at, updated_by) FROM stdin;
    dms               postgres    false    232   |       �          0    17446    document_versions 
   TABLE DATA           �   COPY dms.document_versions (id, name, description, version_number, file_path, document_id, user_id, created_at, created_by, updated_at, updated_by, expired_date, file_size, extension, content_type, encrypt_file) FROM stdin;
    dms               postgres    false    284   �|       �          0    16521 	   documents 
   TABLE DATA           u  COPY dms.documents (id, document_id, file_name, content_type, extension, file_path, collection_id, document_type_id, directory_id, version, file_size, download, owner_id, expired_date, status, created_at, created_by, updated_at, last_updated_at, updated_by, document_name, reference, date_version, document_no, encrypt_file, download_date, delete_date, delete) FROM stdin;
    dms               postgres    false    234   �}       �           0    0     collection_document_types_id_seq    SEQUENCE SET     L   SELECT pg_catalog.setval('dms.collection_document_types_id_seq', 1, false);
          dms               postgres    false    223            �           0    0    collections_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('dms.collections_id_seq', 1, true);
          dms               postgres    false    225            �           0    0    directories_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('dms.directories_id_seq', 2, true);
          dms               postgres    false    227            �           0    0    document_favorites_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('dms.document_favorites_id_seq', 1, false);
          dms               postgres    false    229            �           0    0    document_indices_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('dms.document_indices_id_seq', 3, true);
          dms               postgres    false    231            �           0    0    document_tags_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('dms.document_tags_id_seq', 2, true);
          dms               postgres    false    279            �           0    0    document_types_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('dms.document_types_id_seq', 1, true);
          dms               postgres    false    233            �           0    0    document_versions_id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('dms.document_versions_id_seq', 4, true);
          dms               postgres    false    283            �           0    0    documents_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('dms.documents_id_seq', 2, true);
          dms               postgres    false    235            �           0    0    newtable_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('dms.newtable_id_seq', 9, true);
          dms               postgres    false    276            �           2606    16633 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     x   ALTER TABLE ONLY dms."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 Y   ALTER TABLE ONLY dms."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       dms                 postgres    false    221            �           2606    16635 6   collection_document_types PK_collection_document_types 
   CONSTRAINT     s   ALTER TABLE ONLY dms.collection_document_types
    ADD CONSTRAINT "PK_collection_document_types" PRIMARY KEY (id);
 _   ALTER TABLE ONLY dms.collection_document_types DROP CONSTRAINT "PK_collection_document_types";
       dms                 postgres    false    222            �           2606    16637    collections PK_collections 
   CONSTRAINT     W   ALTER TABLE ONLY dms.collections
    ADD CONSTRAINT "PK_collections" PRIMARY KEY (id);
 C   ALTER TABLE ONLY dms.collections DROP CONSTRAINT "PK_collections";
       dms                 postgres    false    224            �           2606    16639    directories PK_directories 
   CONSTRAINT     W   ALTER TABLE ONLY dms.directories
    ADD CONSTRAINT "PK_directories" PRIMARY KEY (id);
 C   ALTER TABLE ONLY dms.directories DROP CONSTRAINT "PK_directories";
       dms                 postgres    false    226            �           2606    16641 (   document_favorites PK_document_favorites 
   CONSTRAINT     e   ALTER TABLE ONLY dms.document_favorites
    ADD CONSTRAINT "PK_document_favorites" PRIMARY KEY (id);
 Q   ALTER TABLE ONLY dms.document_favorites DROP CONSTRAINT "PK_document_favorites";
       dms                 postgres    false    228            �           2606    16643 $   document_indices PK_document_indices 
   CONSTRAINT     a   ALTER TABLE ONLY dms.document_indices
    ADD CONSTRAINT "PK_document_indices" PRIMARY KEY (id);
 M   ALTER TABLE ONLY dms.document_indices DROP CONSTRAINT "PK_document_indices";
       dms                 postgres    false    230            �           2606    16645     document_types PK_document_types 
   CONSTRAINT     ]   ALTER TABLE ONLY dms.document_types
    ADD CONSTRAINT "PK_document_types" PRIMARY KEY (id);
 I   ALTER TABLE ONLY dms.document_types DROP CONSTRAINT "PK_document_types";
       dms                 postgres    false    232                        2606    16647    documents PK_documents 
   CONSTRAINT     S   ALTER TABLE ONLY dms.documents
    ADD CONSTRAINT "PK_documents" PRIMARY KEY (id);
 ?   ALTER TABLE ONLY dms.documents DROP CONSTRAINT "PK_documents";
       dms                 postgres    false    234                       2606    17420 "   document_delete document_delete_pk 
   CONSTRAINT     m   ALTER TABLE ONLY dms.document_delete
    ADD CONSTRAINT document_delete_pk PRIMARY KEY (document_delete_id);
 I   ALTER TABLE ONLY dms.document_delete DROP CONSTRAINT document_delete_pk;
       dms                 postgres    false    281                       2606    17415 *   document_tags_value document_tags_value_pk 
   CONSTRAINT     x   ALTER TABLE ONLY dms.document_tags_value
    ADD CONSTRAINT document_tags_value_pk PRIMARY KEY (document_tag_value_id);
 Q   ALTER TABLE ONLY dms.document_tags_value DROP CONSTRAINT document_tags_value_pk;
       dms                 postgres    false    280                       2606    17452 &   document_versions document_versions_pk 
   CONSTRAINT     a   ALTER TABLE ONLY dms.document_versions
    ADD CONSTRAINT document_versions_pk PRIMARY KEY (id);
 M   ALTER TABLE ONLY dms.document_versions DROP CONSTRAINT document_versions_pk;
       dms                 postgres    false    284            �   Q   x�32021�04003546����,�L���L/J,�����3�3�2��27612"���0�w�O.�M�+	�,H-�*����� b-�      �      x������ � �      �   i   x�3�0O47I25�4�t���u�Sp�vr�t��.�M�SpJ-�,ITpLN,J�4�4202�54�56T04�24�20�3764�4���-k�K�����+F��� ���      �   ~   x���1
�@E�Sx����c'h%1AH���x�#Ȧ(��o���iMѼ�J�}z�ƹz���/�_�3�T4��ڣB�Nԙ��|�ߕPؚ`k��������������u��4����� z)2$      �      x������ � �      �      x������ � �      �   �   x����j�0E��W�c#�,Y�:k��)���@�B�������]�{f���;��c��. yc_�SD�:��l����9��%�N������;Y
w�,g�dߓ����A�CS	�����$���R�et�."S�RNKԼ�@���5�~
��6����V�+���]B�&;8c{���\Z}u�9�\�y��*����/wkh��!�.P���= 龦�^)!��      �   �   x���A�B1��u{�^�G�4iӝs ���� �{��Q��:B �/�.k�K����	u���z�ma?�����\�C@�A�E��B�HI��p�6��:��?�hE��7�9��bi�Պ���2̄9Zh����J\&V���3�Sg����NP��ɫb��s�����b�+�����,�P�t����Ή,�/"��cN�M�8y�/gh�d      �   �   x�m�M
�0����uyɋ�9D/PK	ƪ-�H��mv-�f�[�Es.���B���BU*nK��S����7Yw\�,�����hx�����m�=u��[���o]�{�9,c,��Ǖ�䠸A�5������i[k���t��uF��1�އ-�c�o!��>U-      �      x������ � �      �   q   x�3�4��HM��L30�tJ-�,ITpLN,J�t��II-Rp��.�M�SI-�N�,Qp��
 
�ʐ����D��H��\��������B�����ܔ3���.)S#�F�=... &�      �      x����n�0E��W�bfƞgT!�C���&�A����:AmUSvH�ƣ_�se�����Or��C�V����¹�l��S�V�P���q��h1}���r9^M�U�$� �a�WL5��`���3{�i�}NS�$�BsA�������|�h���W�������';�������_$c����)��j#bl2���+�t��%�f�����ƬwQ��RF�|ș�ӧ��f���UM�Vo���|t}@0?��}I�>�����lr�@Q��493�p*I�/;ѣ      �     x����n�0E��+��?0�5���<D۝7L�RB��סJE#��J��tg|�{�H�m����/����觳��zw�q8��ڞ�����ڗnr}8^e�\X�>l�vŞ�r�����@� 0���^H��|���d��۶���LAHM��=	F2�9P 3W�TL)håa�p����O�ǹ�u����
�@f& Rγ����x�t-��ޑ2����GwVc�P��(�f�Ǩt�2PKR,��T��̀H8��_�B�;Vl��$��/�]�     
create table localization_resource(
  category   varchar(20),
  key_name   varchar(20),
  ja         varchar(200),
  en         varchar(200),
  constraint pk_localization_resource primary key(
    category, key_name
  )
);

comment on table  localization_resource          is '���ۉ����\�[�X';
comment on column localization_resource.category is '�J�e�S��';
comment on column localization_resource.key_name is '�L�[��';
comment on column localization_resource.ja       is '���{��';
comment on column localization_resource.en       is '�p��';


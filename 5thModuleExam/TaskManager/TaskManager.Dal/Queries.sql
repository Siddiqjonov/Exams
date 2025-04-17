create table ToDoItems (
    Id bigint primary key identity(1,1),
    Title nvarchar(255) not null,
    Description nvarchar(max) not null,
    IsCompleted bit not null,
    CreatedAt datetime2 not null,
    DueDate datetime2 not null
);

go

create procedure spSelectAllToDoItems
    @Skip int,
    @Take int
as
begin
    select 
        Id,
        Title,
        Description,
        IsCompleted,
        CreatedAt,
        DueDate
    from ToDoItems
    order by Id asc
    OFFSET @Skip rows
    fetch next @Take rows ONLY;
end

go

create procedure spAddItem
@Title nvarchar(50),
@Description nvarchar(50),
@IsComplited bit,
@CreatedAt datetime2,
@DueDate datetime2,
@Id bigint output
as
begin
	insert into ToDoItems(Title,Description, IsCompleted, CreatedAt, DueDate)
	values (@Title, @Description, @IsComplited, @CreatedAt, @DueDate)

	set @Id = SCOPE_IDENTITY(); 
end

go

create procedure spDeleteItem @Id bigint
as
begin
	delete from ToDoItems where Id = @id
end

go


	create procedure spupdateitem
    @id bigint,
    @title nvarchar(50),
    @description nvarchar(50),
    @iscomplited bit,
    @duedate datetime2
as
begin
    update todoitems
    set 
        title = @title,
        description = @description,
        iscompleted = @iscomplited,
        duedate = @duedate
    where id = @id;
end

go 

create procedure spgetitembyid
    @id bigint
as
begin
    select 
        id,
        title,
        description,
        iscompleted,
        createdat,
        duedate
    from todoitems
    where id = @id;
end

go

create procedure spselectbyduedatetodoitems
    @duedate datetime2
as
begin
    select 
        id,
        title,
        description,
        iscompleted,
        createdat,
        duedate
    from todoitems
    where duedate = @duedate
end

go

create procedure spselectcompletedtodoitems
    @skip int,
    @take int
as
begin
    select 
        id,
        title,
        description,
        iscompleted,
        createdat,
        duedate
    from todoitems
    where iscompleted = 1
    order by id asc
    offset @skip rows
    fetch next @take rows only;
end

go

create procedure spselectincompletetodoitems
    @skip int,
    @take int
as
begin
    select 
        id,
        title,
        description,
        iscompleted,
        createdat,
        duedate
    from todoitems
    where iscompleted = 0
    order by id asc
    offset @skip rows
    fetch next @take rows only;
end
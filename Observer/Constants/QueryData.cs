namespace Observer.Constants
{
    /// <summary>
    /// List of queries to execute into database.
    /// </summary>
    public class QueryData
    {
        /// <summary>
        /// Insert new user into database.
        /// </summary>
        public const string InsertUsers = @"
insert into 
    dbo.Users
    (
        Name,
        LastName,
        Birthdate,
        Document,
        Login,
        Password,
        CreatedAt,
        UpdatedAt
    )
values
    (
        @Name,
        @LastName,
        @Birthdate,
        @Document,
        @Login,
        @Password,
        @CreatedAt,
        @UpdatedAt
    )

declare @userId int
select @userId = scope_identity()

select
    id,
    Name,
    LastName,
    Birthdate,
    Document,
    Login,
    Password,
    CreatedAt,
    UpdatedAt
from 
    dbo.Users
where
    id = @userId";

        /// <summary>
        /// Select some user by id.
        /// </summary>
        public const string SelectOneUsers = @"
select
    id,
    Name,
    LastName,
    Birthdate,
    Document,
    Login,
    Password,
    CreatedAt,
    UpdatedAt
from 
    dbo.Users
where
    id = @userId";

        /// <summary>
        /// Update some user by id.
        /// </summary>
        public const string UpdateUsers = @"
update
    dbo.Users
set
    Name = @Name,
    Login = @Login,
    LastName = @LastName,
    Document = @Document,
    Password = @Password,
    Birthdate = @Birthdate,
    UpdatedAt = getdate()
where
    id = @userId";

        /// <summary>
        /// Delete some user by id.
        /// </summary>
        public const string DeleteUsers = @"
delete from
    dbo.Users
where
    Id = @userId";

        /// <summary>
        /// Insert new product into database.
        /// </summary>
        public const string InsertProducts = @"
insert into 
    dbo.Products
    (
        Name,
        Category,
        Description,
        Stock,
        CreatedAt,
        UpdatedAt
    )
values
    (
        @Name,
        @Category,
        @Description,
        @Stock,
        @CreatedAt,
        @UpdatedAt
    )

declare @productId int
select @productId = scope_identity()

select
    id,
    Name,
    Category,
    Description,
    Stock,
    CreatedAt,
    UpdatedAt
from 
    dbo.Products
where
    id = @productId";

        /// <summary>
        /// Select some product by id.
        /// </summary>
        public const string SelectOneProducts = @"
select
    id,
    Name,
    Category,
    Description,
    Stock,
    CreatedAt,
    UpdatedAt
from 
    dbo.Products
where
    id = @productId";

        /// <summary>
        /// Update some product by id.
        /// </summary>
        public const string UpdateProducts = @"
update
    dbo.Products
set
    Name = @Name,
    Category = @Category,
    Description = @Description,
    Stock = @Stock,
    UpdatedAt = getdate()
where
    id = @productId";

        /// <summary>
        /// Delete some product by id.
        /// </summary>
        public const string DeleteProducts = @"
delete from
    dbo.Products
where
    Id = @productId";

        /// <summary>
        /// Insert new subscription into database.
        /// </summary>
        public const string InsertSubscriptions = @"
insert into 
    dbo.Subscriptions
    (
        ProductId,
        UserId,
        CreatedAt,
        UpdatedAt
    )
values
    (
        @ProductId,
        @UserId,
        @CreatedAt,
        @UpdatedAt
    )

declare @subscriptionId int
select @subscriptionId = scope_identity()

select
    id,
    ProductId,
    UserId,
    CreatedAt,
    UpdatedAt
from 
    dbo.Subscriptions
where
    id = @subscriptionId";

        /// <summary>
        /// Select some subscription by id.
        /// </summary>
        public const string SelectOneSubscriptions = @"
select
    id,
    ProductId,
    UserId,
    CreatedAt,
    UpdatedAt
from 
    dbo.Subscriptions
where
    id = @subscriptionId";

        /// <summary>
        /// Update some subscription by id.
        /// </summary>
        public const string UpdateSubscriptions = @"
update
    dbo.Subscriptions
set
    ProductId = @ProductId,
    UserId = @UserId,
    UpdatedAt = getdate()
where
    id = @subscriptionId";

        /// <summary>
        /// Delete some subscription by id.
        /// </summary>
        public const string DeleteSubscriptions = @"
delete from
    dbo.Subscriptions
where
    Id = @subscriptionId";
    }
}

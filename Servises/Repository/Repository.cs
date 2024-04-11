using ASPNetCore_WebAPI_BookStore_Website.Data;
using ASPNetCore_WebAPI_BookStore_Website.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.Servises.Repository
{
    public class Repository : IRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly AppSettings _appSettings;

        public static int PAGE_SIZE { get; set; } = 3;


        public Repository(BookStoreDbContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        #region Repo of Author
        public AuthorVM AddAuthor(AuthorVM author)
        {
            var _author = new Authors
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            _context.Add(_author);
            _context.SaveChanges();

            return new AuthorVM
            {
                AuthorId = _author.AuthorId,
                FirstName = _author.FirstName,
                LastName = _author.LastName
            };
        }

        public void DeleteAuthor(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.AuthorId == id);

            if(author != null)
            {
                _context.Remove(author);
                _context.SaveChanges();
            }
        }

        public List<AuthorVM> GetAllAuthor(string search, string sortBy, int page = 1)
        {
            var allAuthors = _context.Authors.AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allAuthors = allAuthors.Where(a => EF.Functions.Like(a.FirstName, $"%{search}%") ||
                EF.Functions.Like(a.LastName, $"%{search}%"));
            }
            #endregion

            #region Sorting
            //Default Sort by Id ASC

            allAuthors = allAuthors.OrderBy(a => a.AuthorId);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_asc":
                        allAuthors = allAuthors.OrderBy(a =>
                        a.FirstName); break;
                    case "name_desc":
                        allAuthors = allAuthors.OrderByDescending(a =>
                        a.FirstName); break;
                }

            }
            #endregion

            var result = PaginatedList<Data.Authors>.Create(allAuthors, page, PAGE_SIZE);

            return result.Select(a => new AuthorVM
            {
                AuthorId = a.AuthorId,
                FirstName = a.FirstName,
                LastName = a.LastName
            }).ToList();
        }

        public AuthorVM GetByIdAuthor(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.AuthorId == id);

            if(author != null)
            {
                return new AuthorVM
                {
                    AuthorId = author.AuthorId,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                };
            }
            else
            {
                return null;
            }
        }

        public void UpdateAuthor(AuthorVM author)
        {
            var _author = _context.Authors.SingleOrDefault(a => a.AuthorId == author.AuthorId);

            if(_author != null)
            {
                _author.FirstName = author.FirstName;
                _author.LastName = author.LastName;

                _context.SaveChanges();
            }
        }
        #endregion

        //----------//

        #region Repo of Book
        public List<BookVM> GetAllBook(string search, string sortBy, int page = 1)
        {
            var allBooks = _context.Books.AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allBooks = allBooks.Where(b => EF.Functions.Like(b.Title, $"%{search}%") ||
                EF.Functions.Like(b.Type, $"%{search}%") ||
                EF.Functions.Like(b.Condition, $"%{search}%") ||
                EF.Functions.Like(b.PublicationYear.ToString(), $"%{search}%"));
            }
            #endregion

            #region Sorting
            //Default Sort by Id ASC

            allBooks = allBooks.OrderBy(b => b.BookId);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "title_asc":
                        allBooks = allBooks.OrderBy(b =>
                        b.Title); break;
                    case "title_desc":
                        allBooks = allBooks.OrderByDescending(b =>
                        b.Title); break;
                    case "price_asc":
                        allBooks = allBooks.OrderBy(b =>
                        b.Price); break;
                    case "price_desc":
                        allBooks = allBooks.OrderByDescending(b =>
                        b.Price); break;
                }

            }
            #endregion

            var result = PaginatedList<Data.Book>.Create(allBooks, page, PAGE_SIZE);

            return result.Select(b => new BookVM
            {
                BookId = b.BookId,
                Title = b.Title,
                Type = b.Type,
                PublicationYear = b.PublicationYear,
                Price = b.Price,
                Condition = b.Condition
            }).ToList();
        }

        public BookVM GetByIdBook(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.BookId == id);

            if (book != null)
            {
                return new BookVM
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Type = book.Type,
                    PublicationYear = book.PublicationYear,
                    Price = book.Price,
                    Condition = book.Condition
                };
            }
            else
            {
                return null;
            }
        }

        public BookVM AddBook(BookVM book)
        {
            var _book = new Book
            {
                Title = book.Title,
                Type = book.Type,
                PublicationYear = book.PublicationYear,
                Price = book.Price,
                Condition = book.Condition
            };

            _context.Add(_book);
            _context.SaveChanges();

            return new BookVM
            {
                BookId = _book.BookId,
                Title = _book.Title,
                Type = _book.Type,
                PublicationYear = _book.PublicationYear,
                Price = _book.Price,
                Condition = _book.Condition
            };
        }

        public void UpdateBook(BookVM book)
        {
            var _book = _context.Books.SingleOrDefault(b => b.BookId == book.BookId);

            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Type = book.Type;
                _book.PublicationYear = book.PublicationYear;
                _book.Price = book.Price;
                _book.Condition = book.Condition;

                _context.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.BookId == id);

            if (book != null)
            {
                _context.Remove(book);
                _context.SaveChanges();
            }
        }
        #endregion

        //----------//

        #region Repo of Customer
        public List<CustomersVM> GetAllCustomer(string search, string sortBy, int page = 1)
        {
            var allCustomers = _context.Customers.AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allCustomers = allCustomers.Where(c => EF.Functions.Like(c.FirstName, $"%{search}%") ||
                EF.Functions.Like(c.LastName, $"%{search}%") ||
                EF.Functions.Like(c.StreetName, $"%{search}%") ||
                EF.Functions.Like(c.StreetNumber, $"%{search}%") ||
                EF.Functions.Like(c.Country, $"%{search}%") ||
                EF.Functions.Like(c.PhoneNumber, $"%{search}%"));
            }
            #endregion

            #region Sorting
            //Default Sort by Id ASC

            allCustomers = allCustomers.OrderBy(c => c.CustomerId);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_asc":
                        allCustomers = allCustomers.OrderBy(c =>
                        c.FirstName); break;
                    case "name_desc":
                        allCustomers = allCustomers.OrderByDescending(c =>
                        c.FirstName); break;
                }
            }
            #endregion

            var result = PaginatedList<Data.Customers>.Create(allCustomers, page, PAGE_SIZE);

            return result.Select(c => new CustomersVM
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                StreetNumber = c.StreetNumber,
                StreetName = c.StreetName,
                Country = c.Country,
                PhoneNumber = c.PhoneNumber
            }).ToList();
        }

        public CustomersVM GetByIdCustomer(int id)
        {
            var customers = _context.Customers.SingleOrDefault(c => c.CustomerId == id);

            if (customers != null)
            {
                return new CustomersVM
                {
                    CustomerId = customers.CustomerId,
                    FirstName = customers.FirstName,
                    LastName = customers.LastName,
                    StreetNumber = customers.StreetNumber,
                    StreetName = customers.StreetName,
                    Country = customers.Country,
                    PhoneNumber = customers.PhoneNumber
                };
            }
            else
            {
                return null;
            }
        }

        public CustomersVM AddCustomer(CustomersVM customers)
        {
            var _customers = new Customers
            {
                FirstName = customers.FirstName,
                LastName = customers.LastName,
                StreetNumber = customers.StreetNumber,
                StreetName = customers.StreetName,
                Country = customers.Country,
                PhoneNumber = customers.PhoneNumber
            };

            _context.Add(_customers);
            _context.SaveChanges();

            return new CustomersVM
            {
                CustomerId = _customers.CustomerId,
                FirstName = _customers.FirstName,
                LastName = _customers.LastName,
                StreetNumber = _customers.StreetNumber,
                StreetName = _customers.StreetName,
                Country = _customers.Country,
                PhoneNumber = _customers.PhoneNumber
            };
        }

        public void UpdateCustomer(CustomersVM customers)
        {
            var _customers = _context.Customers.SingleOrDefault(c => c.CustomerId == customers.CustomerId);

            if (_customers != null)
            {
                _customers.FirstName = customers.FirstName;
                _customers.LastName = customers.LastName;
                _customers.StreetNumber = customers.StreetNumber;
                _customers.StreetName = customers.StreetName;
                _customers.Country = customers.Country;
                _customers.PhoneNumber = customers.PhoneNumber;

                _context.SaveChanges();
            }
        }

        public void DeleteCustomer(int id)
        {
            var customers = _context.Customers.SingleOrDefault(c => c.CustomerId == id);

            if (customers != null)
            {
                _context.Remove(customers);
                _context.SaveChanges();
            }
        }
        #endregion

        //----------//

        #region Repo of Inventory
        public List<InventoryVM> GetAllInventory()
        {
            var inventory = _context.Inventories.Select(i => new InventoryVM
            {
                InventoryId = i.InventoryId,
                BookId = i.BookId,
                StockLevelNew = i.StockLevelNew,
                StockLevelUsed = i.StockLevelUsed
            });
            return inventory.ToList();
        }

        public InventoryVM GetByIdInventory(int id)
        {
            var inventory = _context.Inventories.SingleOrDefault(i => i.InventoryId == id);

            if (inventory != null)
            {
                return new InventoryVM
                {
                    InventoryId = inventory.InventoryId,
                    BookId = inventory.BookId,
                    StockLevelNew = inventory.StockLevelNew,
                    StockLevelUsed = inventory.StockLevelUsed
                };
            }
            else
            {
                return null;
            }
        }

        public InventoryVM AddInventory(InventoryVM inventory)
        {
            var _inventory = new Inventory
            {
                BookId = inventory.BookId,
                StockLevelNew = inventory.StockLevelNew,
                StockLevelUsed = inventory.StockLevelUsed
            };

            _context.Add(_inventory);
            _context.SaveChanges();

            return new InventoryVM
            {
                InventoryId = _inventory.InventoryId,
                BookId = _inventory.BookId,
                StockLevelNew = _inventory.StockLevelNew,
                StockLevelUsed = _inventory.StockLevelUsed
            };
        }

        public void UpdateInventory(InventoryVM inventory)
        {
            var _inventory = _context.Inventories.SingleOrDefault(i => i.InventoryId == inventory.InventoryId);

            if (_inventory != null)
            {
                _inventory.BookId = inventory.BookId;
                _inventory.StockLevelNew = inventory.StockLevelNew;
                _inventory.StockLevelUsed = inventory.StockLevelUsed;

                _context.SaveChanges();
            }
        }

        public void DeleteInventory(int id)
        {
            var inventory = _context.Inventories.SingleOrDefault(i => i.InventoryId == id);

            if (inventory != null)
            {
                _context.Remove(inventory);
                _context.SaveChanges();
            }
        }
        #endregion

        //----------//

        #region Repo of Order
        public List<OrderVM> GetAllOrder()
        {
            var order = _context.Orders.Select(o => new OrderVM
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                SubTotal = o.SubTotal,
                Shipping = o.Shipping,
                Total = o.Total
            });
            return order.ToList();
        }

        public OrderVM GetByIdOrder(int id)
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == id);

            if (order != null)
            {
                return new OrderVM
                {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    SubTotal = order.SubTotal,
                    Shipping = order.Shipping,
                    Total = order.Total
                };
            }
            else
            {
                return null;
            }
        }

        public OrderVM AddOrder(OrderVM order)
        {
            var _order = new Orders
            {
                CustomerId = order.CustomerId,
                OrderDate = DateTime.UtcNow,
                SubTotal = order.SubTotal,
                Shipping = order.Shipping,
                Total = order.Total
            };

            _context.Add(_order);
            _context.SaveChanges();

            return new OrderVM
            {
                OrderId = _order.OrderId,
                CustomerId = _order.CustomerId,
                OrderDate = _order.OrderDate,
                SubTotal = _order.SubTotal,
                Shipping = _order.Shipping,
                Total = _order.Total
            };
        }

        public void UpdateOrder(OrderVM order)
        {
            var _order = _context.Orders.SingleOrDefault(o => o.OrderId == order.OrderId);

            if (_order != null)
            {
                _order.CustomerId = order.CustomerId;
                _order.OrderDate = order.OrderDate;
                _order.SubTotal = order.SubTotal;
                _order.Shipping = order.Shipping;
                _order.Total = order.Total;

                _context.SaveChanges();
            }
        }

        public void DeleteOrder(int id)
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == id);

            if (order != null)
            {
                _context.Remove(order);
                _context.SaveChanges();
            }
        }
        #endregion

        //----------//

        #region Repo of Publisher
        public List<PublisherVM> GetAllPublisher()
        {
            var publisher = _context.Publishers.Select(p => new PublisherVM
            {
                PublisherId = p.PublisherId,
                Country = p.Country,
                BookId = p.BookId
            });
            return publisher.ToList();
        }

        public PublisherVM GetByIdPublisher(int id)
        {
            var publisher = _context.Publishers.SingleOrDefault(p => p.PublisherId == id);

            if (publisher != null)
            {
                return new PublisherVM
                {
                    PublisherId = publisher.PublisherId,
                    Country = publisher.Country,
                    BookId = publisher.BookId
                };
            }
            else
            {
                return null;
            }
        }

        public PublisherVM AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publishers
            {
                Country = publisher.Country,
                BookId = publisher.BookId
            };

            _context.Add(_publisher);
            _context.SaveChanges();

            return new PublisherVM
            {
                PublisherId = _publisher.PublisherId,
                Country = _publisher.Country,
                BookId = _publisher.BookId
            };
        }

        public void UpdatePublisher(PublisherVM publisher)
        {
            var _publisher = _context.Publishers.SingleOrDefault(p => p.PublisherId == publisher.PublisherId);

            if (_publisher != null)
            {
                _publisher.Country = publisher.Country;
                _publisher.BookId = publisher.BookId;

                _context.SaveChanges();
            }
        }

        public void DeletePublisher(int id)
        {
            var publisher = _context.Publishers.SingleOrDefault(p => p.PublisherId == id);

            if (publisher != null)
            {
                _context.Remove(publisher);
                _context.SaveChanges();
            }
        }
        #endregion

        //----------//

        #region Repo of User
        public List<UserVM> GetAllUser()
        {
            var user = _context.Users.Select(u => new UserVM
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role
            });
            return user.ToList();
        }

        public UserVM GetByIdUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);

            if (user != null)
            {
                return new UserVM
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role
                };
            }
            else
            {
                return null;
            }
        }

        public UserVM AddUser(UserVM user)
        {
            var _user = new Users
            {
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Role = user.Role
            };

            _context.Add(_user);
            _context.SaveChanges();

            return new UserVM
            {
                UserId = _user.UserId,
                UserName = _user.UserName,
                Password = _user.Password,
                Email = _user.Email,
                Role = _user.Role
            };
        }

        public void UpdateUser(UserVM user)
        {
            var _user = _context.Users.SingleOrDefault(u => u.UserId == user.UserId);

            if (_user != null)
            {
                _user.UserId = user.UserId;
                _user.UserName = user.UserName;
                _user.Password = user.Password;
                _user.Email = user.Email;
                _user.Role = user.Role;

                _context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);

            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
        }

        public async Task<ApiResponse> Validate(LoginVM model)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == model.UserName && u.Password
            == model.Password);

            if (user == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Username or Password!!!"
                };
            }

            var token = await GenerateToken(user);

            return new ApiResponse
            {
                Success = true,
                Message = "Authenticate Success",
                Data = token
            };
        }

        private async Task<TokenModel> GenerateToken(Users users)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, users.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, users.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, users.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserName", users.UserName),
                    new Claim("Id", users.UserId.ToString()),
                }),

                //set up Token's expire time
                Expires = DateTime.UtcNow.AddSeconds(20),
                //set up Signing
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                (secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var reFreshToken = GenerateRefreshToken();

            var reFreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                JwtId = token.Id,
                UserId = users.UserId,
                Token = reFreshToken,
                IsUsed = false,
                IsRevoked = false,
                IsUsedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddHours(1)
            };

            await _context.AddAsync(reFreshTokenEntity);
            await _context.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = reFreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }

        public async Task<ApiResponse> RenewToken(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                //Signing in Token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,

                ValidateLifetime = false
            };
            try
            {
                //check format access token valid or not
                var tokenVerification = jwtTokenHandler.ValidateToken(model.AccessToken,
                   tokenValidateParam, out var validatedToken);

                //check Token's algorithm
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms
                        .HmacSha512, StringComparison.InvariantCultureIgnoreCase);

                    if (!result)
                    {
                        return new ApiResponse
                        {
                            Success = false,
                            Message = "Invalidate Token!!!"
                        };
                    }
                }

                //check accessToken expire or not
                var utcExpireDate = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type
                == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Access Token has not expire yet!"
                    };
                }

                //check refreshToken was in db or not
                var storedToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == model.RefreshToken);
                if (storedToken == null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh Token has been not found!"
                    };
                }

                //check reFreshToken used, revoked or not?
                if (storedToken.IsUsed)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh Token has been used!"
                    };
                }

                if (storedToken.IsRevoked)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh Token has been revoked!"
                    };
                }

                //check Id of accessToken == JwtId in reFreshToken or not
                var jti = tokenVerification.Claims.FirstOrDefault(x => x.Type ==
                JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Id's Token didn't match!"
                    };
                }

                //update Token isused
                storedToken.IsUsed = true;
                storedToken.IsRevoked = true;
                _context.Update(storedToken);
                await _context.SaveChangesAsync();

                //renew Token
                var user = await _context.Users.SingleOrDefaultAsync(nd => nd.UserId ==
               storedToken.UserId);
                var token = await GenerateToken(user);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Renew Token Successfully!",
                    Data = token
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Something went wrong!!!"
                };
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }
        #endregion

        //----------//

        #region Repo of BookAuthor
        public List<BookAuthorModel> GetAllBA()
        {
            var bookAuthor = _context.BookAuthors.Select(ba => new BookAuthorModel
            {
                BookAuthorsId = ba.BookAuthorsId,
                BookId = ba.BookId,
                AuthorId = ba.AuthorId,
                Title = ba.Book.Title,
                FirstName = ba.Authors.FirstName
            });
            return bookAuthor.ToList();
        }

        public BookAuthorModel GetByIdBA(int id)
        {
            //Trường hợp ta ko tolist kết quả truy vấn, cần thực hiện Include các mối quan hệ bảng cần thiết
            //để có thể lấy dữ liệu tránh mắc lỗi null instance
            var bookAuthor = _context.BookAuthors.Include(ba => ba.Book)
                .Include(ba => ba.Authors).SingleOrDefault(ba => ba.BookAuthorsId == id);

            if (bookAuthor != null)
            {
                return new BookAuthorModel
                {
                    BookAuthorsId = bookAuthor.BookAuthorsId,
                    BookId = bookAuthor.BookId,
                    AuthorId = bookAuthor.AuthorId,
                    Title = bookAuthor.Book.Title,
                    FirstName = bookAuthor.Authors.FirstName
                };
            }
            else
            {
                return null;
            }
        }

        public BookAuthorVM AddBA(BookAuthorVM bookAuthor)
        {
            var _bookAuthor = new BookAuthors
            {
                BookId = bookAuthor.BookId,
                AuthorId = bookAuthor.AuthorId
            };

            _context.Add(_bookAuthor);
            _context.SaveChanges();

            return new BookAuthorVM
            {
                BookAuthorsId = _bookAuthor.BookAuthorsId,
                BookId = _bookAuthor.BookId,
                AuthorId = _bookAuthor.AuthorId
            };
        }

        public void UpdateBA(BookAuthorVM bookAuthor)
        {
            var _bookAuthor = _context.BookAuthors.SingleOrDefault(ba => ba.BookAuthorsId == bookAuthor.BookAuthorsId);

            if (_bookAuthor != null)
            {
                _bookAuthor.BookId = bookAuthor.BookId;
                _bookAuthor.AuthorId = bookAuthor.AuthorId;

                _context.SaveChanges();
            }
        }

        public void DeleteBA(int id)
        {
            var bookAuthor = _context.BookAuthors.SingleOrDefault(ba => ba.BookAuthorsId == id);

            if (bookAuthor != null)
            {
                _context.Remove(bookAuthor);
                _context.SaveChanges();
            }
        }
        #endregion

        //----------//

        #region Repo of OrderItem
        public List<OrderItemModel> GetAllOI()
        {
            var orderItem = _context.OrderItems.Select(oi => new OrderItemModel
            {
               OrderItemId = oi.OrderItemId,
               OrderId = oi.OrderId,
               BookId = oi.BookId,
               Quantity = oi.Quantity,
               Price = oi.Price,
               Title = oi.Book.Title
            });
            return orderItem.ToList();
        }

        public OrderItemModel GetByIdOI(int id)
        {
            var orderItem = _context.OrderItems.SingleOrDefault(oi => oi.OrderItemId == id);

            if (orderItem != null)
            {
                return new OrderItemModel
                {
                    OrderItemId = orderItem.OrderItemId,
                    OrderId = orderItem.OrderId,
                    BookId = orderItem.BookId,
                    Quantity = orderItem.Quantity,
                    Price = orderItem.Price,
                    Title = orderItem.Book.Title
                };
            }
            else
            {
                return null;
            }
        }

        public OrderItemVM AddOI(OrderItemVM orderItem)
        {
            var _orderItem = new OrderItem
            {
                OrderId = orderItem.OrderId,
                BookId = orderItem.BookId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };

            _context.Add(_orderItem);
            _context.SaveChanges();

            return new OrderItemVM
            {
                OrderItemId = _orderItem.OrderItemId,
                OrderId = _orderItem.OrderId,
                BookId = _orderItem.BookId,
                Quantity = _orderItem.Quantity,
                Price = _orderItem.Price
            };
        }

        public void UpdateOI(OrderItemVM orderItem)
        {
            var _orderItem = _context.OrderItems.SingleOrDefault(oi => oi.OrderItemId == orderItem.OrderItemId);

            if (_orderItem != null)
            {
                _orderItem.OrderId = orderItem.OrderId;
                _orderItem.BookId = orderItem.BookId;
                _orderItem.Quantity = orderItem.Quantity;
                _orderItem.Price = orderItem.Price;

                _context.SaveChanges();
            }
        }

        public void DeleteOI(int id)
        {
            var orderItem = _context.OrderItems.SingleOrDefault(oi => oi.OrderItemId == id);

            if (orderItem != null)
            {
                _context.Remove(orderItem);
                _context.SaveChanges();
            }
        }
        #endregion
    }
}

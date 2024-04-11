using ASPNetCore_WebAPI_BookStore_Website.Data;
using ASPNetCore_WebAPI_BookStore_Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.Servises.Repository
{
    public interface IRepository
    {
        #region Repo of Authors
        List<AuthorVM> GetAllAuthor(string search, string sortBy, int page = 1);
        AuthorVM GetByIdAuthor(int id);
        AuthorVM AddAuthor(AuthorVM author);
        void UpdateAuthor(AuthorVM author);
        void DeleteAuthor(int id);
        #endregion

        #region Repo of Book
        List<BookVM> GetAllBook(string search, string sortBy, int page = 1);
        BookVM GetByIdBook(int id);
        BookVM AddBook(BookVM book);
        void UpdateBook(BookVM book);
        void DeleteBook(int id);
        #endregion

        #region Repo of Customer
        List<CustomersVM> GetAllCustomer(string search, string sortBy, int page = 1);
        CustomersVM GetByIdCustomer(int id);
        CustomersVM AddCustomer(CustomersVM customers);
        void UpdateCustomer(CustomersVM customers);
        void DeleteCustomer(int id);
        #endregion

        #region Repo of Inventory
        List<InventoryVM> GetAllInventory();
        InventoryVM GetByIdInventory(int id);
        InventoryVM AddInventory(InventoryVM inventory);
        void UpdateInventory(InventoryVM inventory);
        void DeleteInventory(int id);
        #endregion

        #region Repo of Order
        List<OrderVM> GetAllOrder();
        OrderVM GetByIdOrder(int id);
        OrderVM AddOrder(OrderVM order);
        void UpdateOrder(OrderVM order);
        void DeleteOrder(int id);
        #endregion

        #region Repo of Publisher
        List<PublisherVM> GetAllPublisher();
        PublisherVM GetByIdPublisher(int id);
        PublisherVM AddPublisher(PublisherVM publisher);
        void UpdatePublisher(PublisherVM publisher);
        void DeletePublisher(int id);
        #endregion

        #region Repo of User
        List<UserVM> GetAllUser();
        UserVM GetByIdUser(int id);
        UserVM AddUser(UserVM user);
        void UpdateUser(UserVM user);
        void DeleteUser(int id);
        Task<ApiResponse>Validate(LoginVM model);
        Task<ApiResponse>RenewToken(TokenModel model);
        #endregion

        #region Repo of BookAuthor
        List<BookAuthorModel> GetAllBA();
        BookAuthorModel GetByIdBA(int id);
        BookAuthorVM AddBA(BookAuthorVM bookAuthor);
        void UpdateBA(BookAuthorVM bookAuthor);
        void DeleteBA(int id);
        #endregion

        #region Repo of OrderItem
        List<OrderItemModel> GetAllOI();
        OrderItemModel GetByIdOI(int id);
        OrderItemVM AddOI(OrderItemVM orderItem);
        void UpdateOI(OrderItemVM orderItem);
        void DeleteOI(int id);
        #endregion
    }
}

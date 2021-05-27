using System.ServiceModel;
using WebServices.Models;

namespace WebServices.SoapServices
{
    [ServiceContract]
    public interface IBooksService
    {
        [OperationContract(Name = "GetBookByIsbn")]
        Book GetBookByIsbn(long isbn);

        [OperationContract(Name = "RegisterNewBook")]
        void RegisterNewBook(Book book);

        [OperationContract(Name = "GetAuthorByIsbn")]
        string GetAuthorByIsbn(long isbn);
    }
}
using FluentValidation;
using Torc.Assesment.Dal;
using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Api.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderModel>
    {
        private readonly IUnityOfWork _unityOfWork;

        public CreateOrderValidator(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
            RuleFor(createOrder => createOrder.ProductId).NotEmpty().GreaterThan(0).Must(ProductIdExists).WithMessage("The product id is required, be greater than zero and exists in the system records.");
            RuleFor(createOrder => createOrder.CustomerId).NotEmpty().GreaterThan(0).Must(CustomerIdExists).WithMessage("The customer id is required, be greater than zero and exists in the system records.");
            RuleFor(createOrder => createOrder.Quantity).NotEmpty().GreaterThan(0).WithMessage("The quantity of products is required and should be greater than zero.");
        }


        private bool ProductIdExists(int productId)
        {
            return _unityOfWork.ProductRepository.GetByIdAsync(productId).Result != null;
        }

        private bool CustomerIdExists(int customerId)
        {
            return _unityOfWork.CustomerRepository.GetByIdAsync(customerId).Result != null;
        }
    }
}

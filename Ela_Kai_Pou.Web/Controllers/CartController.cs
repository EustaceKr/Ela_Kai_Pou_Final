using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Entities.Models;
using Ela_Kai_Pou.Web.Areas.Admin.Services;
using Microsoft.AspNet.Identity;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ela_Kai_Pou.Web.Controllers
{
    public class CartController : Controller
    {
        //Properties
        private readonly IUnitOfWork _unitOfWork;
        private Payment payment;

        //Constructor
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = _unitOfWork.CartRepository.GetCart(User.Identity.GetUserId());
            _unitOfWork.OrderItemRepository.GetAll().Where(y => y.Cart.Id == cart.Id).Select(c => c);
            TempData["FinalCart"] = cart;
            if (cart.OrderItems.Count == 0)
            {
                ViewBag.Message = "Your cart is empty.";
            }
            else
            {
                ViewBag.GrandTotal = _unitOfWork.CartRepository.CartTotalCost(cart);
            }
            return View(cart);
        }

        public ActionResult RemoveOrderItem(int id)
        {
            OrderItem orderItem = _unitOfWork.OrderItemRepository.Get(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }

            return View(orderItem);
        }

        public ActionResult RemoveItem(int id)
        {
            Cart cart = _unitOfWork.CartRepository.GetCart(User.Identity.GetUserId());
            Coffee coffee = _unitOfWork.CoffeeRepository.Get(id);

            _unitOfWork.OrderItemRepository.GetAll()
            .Where(y => y.Cart.Id == cart.Id)
            .Select(c => c);

            OrderItem orderItem = cart.OrderItems.Where(l => l.Product.Id == coffee.Id).FirstOrDefault();
            _unitOfWork.OrderItemRepository.Delete(orderItem);
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        public ActionResult AddQuantity(int id, int quantity = 1)
        {
            Cart cart = _unitOfWork.CartRepository.GetCart(User.Identity.GetUserId());
            Coffee coffee = _unitOfWork.CoffeeRepository.Get(id);

            _unitOfWork.OrderItemRepository.GetAll()
            .Where(y => y.Cart.Id == cart.Id)
            .Select(c => c);

            cart = _unitOfWork.CartRepository.AddQuantity(cart, coffee, quantity);
            _unitOfWork.CartRepository.Update(cart);
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveQuantity(int id, int quantity = 1)
        {
            Cart cart = _unitOfWork.CartRepository.GetCart(User.Identity.GetUserId());
            Coffee coffee = _unitOfWork.CoffeeRepository.Get(id);

            _unitOfWork.OrderItemRepository.GetAll()
            .Where(y => y.Cart.Id == cart.Id)
            .Select(c => c);

            cart = _unitOfWork.CartRepository.RemoveQuantity(cart, coffee, quantity);
            _unitOfWork.CartRepository.Update(cart);
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
        public ActionResult CompleteOrder()
        {
            var cart = _unitOfWork.CartRepository.GetCart(User.Identity.GetUserId());
            _unitOfWork.OrderItemRepository.GetAll().Where(y => y.Cart.Id == cart.Id).Select(c => c);
            var list = cart.OrderItems.ToList();
            cart.TotalPrice = cart.GetTotalPrice();
            var order = _unitOfWork.OrderRepository.GetOrderFromCart(cart);
            foreach (var item in list)
            {
                ProductService.PlaceInOrder((Coffee)item.Product, _unitOfWork);
                _unitOfWork.OrderItemRepository.Add(item);
            }
            _unitOfWork.OrderRepository.Add(order);

            _unitOfWork.CartRepository.ClearCartItems(cart);
            _unitOfWork.OrderItemRepository.DeleteAllOldCartOrderItems(cart);

            _unitOfWork.CartRepository.Update(cart);
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CountItems()
        {
            Cart cart = _unitOfWork.CartRepository.GetCart(User.Identity.GetUserId());
            _unitOfWork.OrderItemRepository.GetAll().Where(y => y.Cart.Id == cart.Id).Select(c => c);
            int model = _unitOfWork.CartRepository.CountItems(cart);

            return PartialView("_Cart", model);
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Cart/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Failure");
            }

            var cart = _unitOfWork.CartRepository.GetCart(User.Identity.GetUserId());
            _unitOfWork.OrderItemRepository.GetAll().Where(y => y.Cart.Id == cart.Id).Select(c => c);
            var list = cart.OrderItems.ToList();
            cart.TotalPrice = cart.GetTotalPrice();
            var order = _unitOfWork.OrderRepository.GetOrderFromCart(cart);
            foreach (var item in list)
            {
                item.Product.IsInOrder = true;
                _unitOfWork.CoffeeRepository.Update((Coffee)item.Product);
                _unitOfWork.OrderItemRepository.Add(item);
            }
            _unitOfWork.OrderRepository.Add(order);

            _unitOfWork.CartRepository.ClearCartItems(cart);
            _unitOfWork.OrderItemRepository.DeleteAllOldCartOrderItems(cart);

            _unitOfWork.CartRepository.Update(cart);
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return View("Error");
            }

            //on successful payment, show success page to user.  
            return View("Success");
        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            var cart = _unitOfWork.CartRepository.GetCart(User.Identity.GetUserId());
            _unitOfWork.OrderItemRepository.GetAll().Where(y => y.Cart.Id == cart.Id).Select(c => c);

            foreach (var orderItem in cart.OrderItems)
            {
                var item = new Item()
                {
                    name = orderItem.Product.Name,
                    currency = "EUR",
                    price = orderItem.Product.Price.ToString("0.00", new System.Globalization.CultureInfo("en-US")),
                    quantity = orderItem.Quantity.ToString(),
                    sku = "CoffeeNumber" + " " + orderItem.Product_Id.ToString()
                };
                itemList.items.Add(item);
            }
            
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details
            var totalPrice = Math.Round(cart.GetTotalPrice(), 1).ToString("0.00", new System.Globalization.CultureInfo("en-US"));
            //string price = item.Preco.ToString("0.00", new System.Globalization.CultureInfo("en-US"));
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = totalPrice
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "EUR",
                total = totalPrice, // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "ELA_KAI_POU order" + cart.Id.ToString(),
                invoice_number = Convert.ToString((new Random()).Next(100000)), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);

        }
    }
}
using EBookStore.EBookStore.ORM;
using EBookStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Managers
{
    public class PaymentManager
    {
        public Payment GetPayment()
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var payment = contextModel.Payments.FirstOrDefault();

                    return payment;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PaymentManager.GetPayment", ex);
                throw;
            }
        }

        public Payment GetPayment(Guid paymentID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var payment = contextModel.Payments
                        .Where(item => item.PaymentID == paymentID)
                        .FirstOrDefault();

                    return payment;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PaymentManager.GetPayment", ex);
                throw;
            }
        }

        public List<Payment> GetPaymentList()
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var paymentList = contextModel.Payments
                        .Select(item => item)
                        .ToList();

                    return paymentList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PaymentManager.GetPaymentList", ex);
                throw;
            }
        }

        public void CreatePayment(Guid paymentID, string paymentName)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    Payment newPayment = new Payment()
                    {
                        PaymentID = paymentID,
                        PaymentName = paymentName,
                        PaymentDate = DateTime.Now,
                    };

                    contextModel.Payments.Add(newPayment);
                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PaymentManager.CreatePayment", ex);
                throw;
            }
        }

        public void UpdatePayment(Guid paymentID, string paymentName)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var toUpdatePayment = contextModel.Payments
                        .Where(item => item.PaymentID == paymentID)
                        .FirstOrDefault();
                    toUpdatePayment.PaymentName = paymentName;

                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PaymentManager.UpdatePayment", ex);
                throw;
            }
        }

        public void BatchDeletePayment(List<Guid> paymentIDList)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var toDeletePaymentList = paymentIDList
                        .Select(paymentID => contextModel.Payments
                        .Where(payment => payment.PaymentID == paymentID)
                        .FirstOrDefault())
                        .ToList();

                    contextModel.Payments.RemoveRange(toDeletePaymentList);
                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PaymentManager.UpdatePayment", ex);
                throw;
            }
        }
    }
}
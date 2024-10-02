using FinanceFlow.Domain.Enums;
using FinanceFlow.Domain.Reports;

namespace FinanceFlow.Domain.Extensions;

public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceReportGenerationMessage.PAYMENT_TYPE_CASH,
            PaymentType.CreditCard => ResourceReportGenerationMessage.PAYMENT_TYPE_CREDIT_CARD,
            PaymentType.DebitCard => ResourceReportGenerationMessage.PAYMENT_TYPE_DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourceReportGenerationMessage.PAYMENT_TYPE_ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}

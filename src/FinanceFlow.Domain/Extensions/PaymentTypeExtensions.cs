using FinanceFlow.Domain.Enums;
using FinanceFlow.Domain.Reports;

namespace FinanceFlow.Domain.Extensions;

public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentsType paymentType)
    {
        return paymentType switch
        {
            PaymentsType.Cash => ResourceReportGenerationMessage.PAYMENT_TYPE_CASH,
            PaymentsType.CreditCard => ResourceReportGenerationMessage.PAYMENT_TYPE_CREDIT_CARD,
            PaymentsType.DebitCard => ResourceReportGenerationMessage.PAYMENT_TYPE_DEBIT_CARD,
            PaymentsType.EletronicTransfer => ResourceReportGenerationMessage.PAYMENT_TYPE_ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}

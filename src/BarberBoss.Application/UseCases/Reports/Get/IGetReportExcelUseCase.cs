using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.Reports.Get;
public interface IGetReportExcelUseCase
{
    Task<byte[]> Execute(RequestReportJson request);
}

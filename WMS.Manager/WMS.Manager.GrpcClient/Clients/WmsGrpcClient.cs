
using System;

namespace WMS.Manager.GrpcClient.Clients
{
    /// <summary>
    /// Представляет клиент Grpc.
    /// </summary>
    public class WmsGrpcClient
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="WmsGrpcClient"/>.
        /// </summary>
        public WmsGrpcClient()
        {
            Nomenclature = new NomenclatureGrpcClient("http://localhost:8010");
            NomenclatureType = new NomenclatureTypeGrpcClient("http://localhost:8010");
        }

        /// <summary>
        /// Клиент номенклатуры.
        /// </summary>
        public NomenclatureGrpcClient Nomenclature { get; }

        /// <summary>
        /// Клиент типа номенклатуры.
        /// </summary>
        public NomenclatureTypeGrpcClient NomenclatureType { get; }

        /// <summary>
        /// Устанавливает обработчик исключения.
        /// </summary>
        public void SetExceptionHandler(Action<Exception> exceptionHandler)
        {
            Nomenclature.SetExceptionHandler(exceptionHandler);
            NomenclatureType.SetExceptionHandler(exceptionHandler);
        }
    }
}
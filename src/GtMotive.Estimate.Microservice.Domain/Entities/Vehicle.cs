namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Representa un vehículo con sus propiedades y estado de alquiler.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets el identificador único del vehículo.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets el modelo del vehículo.
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets el año de fabricación.
        /// </summary>
        public int YearOfManufacture { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether está actualmente alquilado.
        /// </summary>
        public bool IsRented { get; set; }

        /// <summary>
        /// Gets or sets el Id del usuario que lo ha alquilado (si procede).
        /// </summary>
        public string RentedBy { get; set; }
    }
}

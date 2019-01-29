namespace Organizations.Dtos
{
    public class AddressDto
    {
        public int ID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }
        public string TypeOfAddress { get; set; }
    }
}

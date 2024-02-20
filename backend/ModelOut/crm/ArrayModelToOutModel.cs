namespace printing_calculator.ModelOut.crm
{
    public static class ArrayModelToOutModel
    {
        public static Contact[] ContactConvert(DataBase.crm.Contact[] contacts) 
        {
            int contactCount = contacts.Length;
            Contact[] outContact = new Contact[contactCount];

            for (int i = 0; i < contactCount; i++)
            {
                outContact[i] = (Contact)contacts[i];
            }

            return outContact;
        }

        public static Order[] OrderConvert(DataBase.crm.Order[] order)
        {
            int contactCount = order.Length;
            Order[] outOrder = new Order[contactCount];

            for (int i = 0; i < contactCount; i++)
            {
                outOrder[i] = (Order)order[i];
            }

            return outOrder;
        }
    }
}
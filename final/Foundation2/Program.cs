//I'm currently working on the Foundation 4 project option, specifically focusing on the Encapsulation program. So far, 
//I have completed the implementation of classes such as Address, Customer, Product, and Order. These classes have methods and attributes 
//defined as per the program specification, allowing me to calculate total costs, generate packing and shipping labels, and handle customer and product 
//information effectively.
using System;

class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsUSA()
    {
        return country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public string GetName()
    {
        return name;
    }

    public Address GetAddress()
    {
        return address;
    }

    public bool IsUSACustomer()
    {
        return address.IsUSA();
    }
}

class Product
{
    private string name;
    private int productId;
    private decimal price;
    private int quantity;

    public Product(string name, int productId, decimal price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public string GetName()
    {
        return name;
    }

    public int GetProductId()
    {
        return productId;
    }

    public decimal GetPrice()
    {
        return price;
    }

    public int GetQuantity()
    {
        return quantity;
    }

    public decimal GetTotalCost()
    {
        return price * quantity;
    }
}

class Order
{
    private Customer customer;
    private Product[] products;

    public Order(Customer customer)
    {
        this.customer = customer;
        this.products = new Product[0];
    }

    public void AddProduct(Product product)
    {
        Array.Resize(ref products, products.Length + 1);
        products[products.Length - 1] = product;
    }

    public Customer GetCustomer()
    {
        return customer;
    }

    public Product[] GetProducts()
    {
        return products;
    }

    public decimal CalculateTotalCost()
    {
        decimal totalCost = 0;
        foreach (var product in products)
        {
            totalCost += product.GetTotalCost();
        }

        decimal shippingCost = customer.IsUSACustomer() ? 5 : 35;
        return totalCost + shippingCost;
    }

    public string GetPackingLabel()
    {
        string packingLabel = "";
        foreach (var product in products)
        {
            packingLabel += $"Name: {product.GetName()}, Product ID: {product.GetProductId()}\n";
        }
        return packingLabel;
    }

    public string GetShippingLabel()
    {
        var customerAddress = customer.GetAddress();
        return $"Customer Name: {customer.GetName()}\nAddress:\n{customerAddress.GetFullAddress()}";
    }
}

class Program
{
    static void Main()
    {
        // Create addresses
        var address1 = new Address("123 Main St", "Anytown", "CA", "USA");
        var address2 = new Address("456 Elm St", "Anycity", "NY", "Canada");

        // Create customers
        var customer1 = new Customer("Alice", address1);
        var customer2 = new Customer("Bob", address2);

        // Create products
        var product1 = new Product("Shoes", 101, 50, 2);
        var product2 = new Product("Shirt", 102, 20, 3);
        var product3 = new Product("Hat", 103, 10, 1);

        // Create orders
        var order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        var order2 = new Order(customer2);
        order2.AddProduct(product2);
        order2.AddProduct(product3);

        // Display results
        Console.WriteLine("Order 1 Packing Label:\n" + order1.GetPackingLabel());
        Console.WriteLine("Order 1 Shipping Label:\n" + order1.GetShippingLabel());
        Console.WriteLine("Order 1 Total Cost: $" + order1.CalculateTotalCost());

        Console.WriteLine("\nOrder 2 Packing Label:\n" + order2.GetPackingLabel());
        Console.WriteLine("Order 2 Shipping Label:\n" + order2.GetShippingLabel());
        Console.WriteLine("Order 2 Total Cost: $" + order2.CalculateTotalCost());
    }
}

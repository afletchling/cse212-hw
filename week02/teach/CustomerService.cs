using System.Diagnostics;

/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Check if the queue size of a non positive size argument is 10
        // Expected Result:
        Console.WriteLine("Test 1");
        var cs = new CustomerService(0);
        for (int iterations = 0; iterations < 10; iterations++)
        {
            cs.AddNewCustomer();
        }

        // Defect(s) Found:
        try
        {
            for (int iterations = 0; iterations < 10; iterations++)
            {
                cs.ServeCustomer();
            }
        }
        catch (ArgumentOutOfRangeException exception)
        {
            Trace.Assert(false, "Queue size was not 10");
        }

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Adding and removing a customer
        // Expected Result:
        Console.WriteLine("Test 2");
        cs = new CustomerService(1);
        cs.AddNewCustomer();

        // Defect(s) Found:
        try
        {
            cs.ServeCustomer();
        }
        catch (ArgumentOutOfRangeException exception)
        {
            Trace.Assert(false, "Unable to find and process a customer");
        }

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Checking that only one customer can be added to the queue with a size of one
        Console.WriteLine("Test 3");
        cs = new CustomerService(1);
        cs.AddNewCustomer();
        cs.AddNewCustomer();

        try
        {
            cs.ServeCustomer();
            cs.ServeCustomer();
            Trace.Assert(false, "Successfully served two customers with a queue size of one");
        }
        catch (ArgumentOutOfRangeException exception)
        {

        }

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Trying to process a non existent customer
        Console.WriteLine("Test 4");
        cs = new CustomerService(1);
        try
        {
            cs.ServeCustomer();
            Trace.Assert(false, "Successfully served a non existent customer");
        }
        catch (ArgumentOutOfRangeException exception)
        {

        }
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        var customer = _queue[0];
        Console.WriteLine(customer);
        _queue.RemoveAt(0);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}
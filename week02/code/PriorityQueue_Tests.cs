using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Verifing if the priority number enforces order
    // Expected Result: "hello" "thank you" "goodbye"
    // Defect(s) Found: Items were not being removed.
    public void TestPriorityQueue_CorrectOrder()
    {
        string[] testOrder = ["hello", "thank you", "goodbye"];
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("goodbye", 1);
        priorityQueue.Enqueue("thank you", 2);
        priorityQueue.Enqueue("hello", 3);

        foreach (string compare in testOrder)
        {
            Assert.AreEqual(compare, priorityQueue.Dequeue());
        }
    }

    [TestMethod]
    // Scenario: Correctly ensuring that all entries are respected in the proper order with same priority items.
    // Expected Result: "hello" "hola" "bonjour" "thank you" "gracias" "goodbye" "adios"
    // Defect(s) Found: Dequeue was not reaching the start and end of the queue.
    public void TestPriorityQueue_CorrectPriorityOrdering()
    {
        string[] testOrder = ["bonjour", "hola", "hello", "gracias", "thank you", "adios", "goodbye"];
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("adios", 1);
        priorityQueue.Enqueue("goodbye", 1);
        priorityQueue.Enqueue("gracias", 2);
        priorityQueue.Enqueue("thank you", 2);
        priorityQueue.Enqueue("bonjour", 3);
        priorityQueue.Enqueue("hola", 3);
        priorityQueue.Enqueue("hello", 3);

        foreach (string compare in testOrder)
        {
            Assert.AreEqual(compare, priorityQueue.Dequeue());
        }
    }

    [TestMethod]
    // Scenario: Test if a exception is thrown when removing from a empty queue
    // Expected Result: "hello" "goodbye"
    // Defect(s) Found:
    public void TestPriorityQueue_NonEmptyQueue()
    {
        string[] testOrder = ["hello", "thank you"];
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("thank you", 1);
        priorityQueue.Enqueue("hello", 2);

        foreach (string compare in testOrder)
        {
            Assert.AreEqual(compare, priorityQueue.Dequeue());
        }

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Succeeded in removing from an empty queue");
        }
        catch (Exception exception)
        {
            Assert.IsTrue(exception is InvalidOperationException, "Invalid exception for this operation.");
            Assert.AreEqual("The queue is empty.", exception.Message);
        }
    }
}
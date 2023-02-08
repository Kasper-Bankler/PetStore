using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PetStore;

namespace PetStore
{
    internal class Program
    {
        public static List<CustomerModel> CustomerList = new List<CustomerModel>();
        public static List<ProductModel> ProductList = new List<ProductModel>();
        public static List<StoreModel> StoreList = new List<StoreModel>();
        static void Main(string[] args)
        {
            bool isProgramExiting = false, isInputCorrect = false;

            Console.WriteLine("Welcome to the Pet Store");

            while (!isProgramExiting)
            {
                Console.WriteLine("\n");
                Console.WriteLine("You have the folllowing options:");
                Console.WriteLine("Press 1 to display all customers");
                Console.WriteLine("Press 2 to add a new customer");
                Console.WriteLine("Press 3 to display all stores");
                Console.WriteLine("Press 4 to display all products");
                Console.WriteLine("Press 5 to remove a product");
                Console.WriteLine("Press 6 to display all products under a specific price");
                Console.WriteLine("Press 7 to sort by category");
                Console.WriteLine("Press 8 to sort by a specific store");
                Console.WriteLine("Press 0 to exit program");

                int numberInput = -1;
                isInputCorrect = false;

                while (!isInputCorrect)
                {
                    string userInput = Console.ReadLine();

                    try
                    {
                        numberInput = int.Parse(userInput);
                        if (numberInput >= 0 && numberInput <= 8)
                        {
                            isInputCorrect = true;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Please enter a number for which menu item you want to use");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a number between 0 and 8");
                    }
                }

                switch (numberInput)
                {
                    case 0:
                        Console.WriteLine("Program is now exiting");
                        isProgramExiting = true;
                        Console.ReadKey();
                        break;
                    case 1:
                        Console.Clear();
                        CustomerList = SqliteDataAccess.LoadCustomers();

                        for (int i = 0; i < CustomerList.Count; i++)
                        {
                            Console.WriteLine(CustomerList[i].CustomerID + ": " + CustomerList[i].CustomerName);
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Please enter the name of the new customer:");
                        string customerNameText = Console.ReadLine();
                        CustomerModel p = new CustomerModel();

                        p.CustomerName = customerNameText;

                        SqliteDataAccess.SaveCustomer(p);
                        break;
                    case 3:
                        Console.Clear();
                        StoreList = SqliteDataAccess.LoadStores();

                        for (int i = 0; i < StoreList.Count; i++)
                        {
                            Console.WriteLine(StoreList[i].StoreID + ": " + StoreList[i].StoreName);
                        }
                        break;
                    case 4:
                        Console.Clear();
                        ProductList = SqliteDataAccess.LoadProducts();

                        for (int i = 0; i < ProductList.Count; i++)
                        {
                            Console.WriteLine("ID: " + ProductList[i].ProductID + ", Breed: " + ProductList[i].ProductRace + ", Price: " + ProductList[i].ProductPrice);
                        }
                        break;
                    case 5:
                        Console.Clear();
                        try
                        {
                            Console.WriteLine("Please enter the ID of the product that you want to remove:");
                            int ProductIDText = int.Parse(Console.ReadLine());
                            ProductModel pr = new ProductModel();

                            pr.ProductID = ProductIDText;

                            SqliteDataAccess.RemoveProduct(pr);
                            Console.WriteLine("The product has successfully been removed");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Please enter a valid product ID");
                        }
                        break;
                    case 6:
                        Console.Clear();
                        ProductList = SqliteDataAccess.LoadProducts();
                        try
                        {
                            Console.WriteLine("Please enter the maximum price that you want to sort by: ");
                            int MaxPrice = int.Parse(Console.ReadLine());
                            var result = from element in ProductList
                                         where element.ProductPrice < MaxPrice
                                         select element;
                            List<ProductModel> resultList = result.ToList();
                            for (int i = 0; i < resultList.Count; i++)
                            {
                                Console.WriteLine("ProductID: " + resultList[i].ProductID + " Price: " + resultList[i].ProductPrice + " Category: " + resultList[i].ProductCategory);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Please enter a valid maximum price");
                        }
                        break;
                    case 7:
                        Console.Clear();
                        ProductList = SqliteDataAccess.LoadProducts();
                        try
                        {
                            Console.WriteLine("Please enter the category that you want to view (Cat or Dog)");
                            string SelectedCategory = Console.ReadLine();
                            var result2 = from element in ProductList
                                          where element.ProductCategory == SelectedCategory
                                          select element;
                            List<ProductModel> resultList2 = result2.ToList();
                            for (int i = 0; i < resultList2.Count; i++)
                            {
                                Console.WriteLine("ProductID: " + resultList2[i].ProductID + " Price: " + resultList2[i].ProductPrice + " Category: " + resultList2[i].ProductCategory);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Please enter a valid category");
                        }
                        break;
                    case 8:
                        Console.Clear();
                        ProductList = SqliteDataAccess.LoadProducts();
                        try
                        {
                            Console.WriteLine("Please enter the ID of the store (1 or 2)");
                            int StoreID = int.Parse(Console.ReadLine());
                            if (StoreID == 1)
                            {
                                var result3 = from element in ProductList
                                              where element.ProductID == 1
                                              select element;
                                List<ProductModel> resultList3 = result3.ToList();
                                for (int i = 0; i < resultList3.Count; i++)
                                {
                                    Console.WriteLine("Name: " + resultList3[i].ProductCategory + ", Price: " + resultList3[i].ProductPrice);
                                }
                            }
                            else if (StoreID == 2)
                            {
                                var result4 = from element in ProductList
                                              where element.ProductID == 2
                                              select element;
                                List<ProductModel> resultList4 = result4.ToList();
                                for (int i = 0; i < resultList4.Count; i++)
                                {
                                    Console.WriteLine("Name: " + resultList4[i].ProductCategory + ", Price: " + resultList4[i].ProductPrice);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Please enter a valid store ID");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

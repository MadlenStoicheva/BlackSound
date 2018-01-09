﻿using BlackSound.Entity;
using BlackSound.Repository;
using BlackSound.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.View
{
    public class UserManagementView
    {
            public void Show()
            {
                while (true)
                {
                    UserManagementEnum choice = RenderMenu();

                    try
                    {
                        switch (choice)
                        {
                            case UserManagementEnum.Select:
                                {
                                    GetAll();
                                    break;
                                }
                            case UserManagementEnum.View:
                                {
                                    View();
                                    break;
                                }
                            case UserManagementEnum.Insert:
                                {
                                    Add();
                                    break;
                                }
                            case UserManagementEnum.Update:
                                {
                                    Update();
                                    break;
                                }
                            case UserManagementEnum.Delete:
                                {
                                    Delete();
                                    break;
                                }
                            case UserManagementEnum.Exit:
                                {
                                    return;
                                }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey(true);
                    }
                }
            }

            private UserManagementEnum RenderMenu()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Users management:");
                    Console.WriteLine("[G]et all Users");
                    Console.WriteLine("[V]iew User");
                    Console.WriteLine("[A]dd User");
                    Console.WriteLine("[E]dit User");
                    Console.WriteLine("[D]elete User");
                    Console.WriteLine("E[x]it");

                    string choice = Console.ReadLine();
                    switch (choice.ToUpper())
                    {
                        case "G":
                            {
                                return UserManagementEnum.Select;
                            }
                        case "V":
                            {
                                return UserManagementEnum.View;
                            }
                        case "A":
                            {
                                return UserManagementEnum.Insert;
                            }
                        case "E":
                            {
                                return UserManagementEnum.Update;
                            }
                        case "D":
                            {
                                return UserManagementEnum.Delete;
                            }
                        case "X":
                            {
                                return UserManagementEnum.Exit;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid choice.");
                                Console.ReadKey(true);
                                break;
                            }
                    }
                }
            }

            private void GetAll()
            {
                Console.Clear();

                UserRepository usersRepository = new UserRepository("users.txt");
                List<User> users = usersRepository.GetAll();

                foreach (User user in users)
                {
                    Console.WriteLine("ID:" + user.Id);
                    Console.WriteLine("Username :" + user.Username);
                    Console.WriteLine("Password :" + user.Password);
                    Console.WriteLine("Email :" + user.Email);
                    Console.WriteLine("Display Name :" + user.DisplayName);
                    Console.WriteLine("Is Admin:" + user.IsAdmin);

                    Console.WriteLine("-------------------------------------");
                }

                Console.ReadKey(true);
            }

            private void View()
            {
                Console.Clear();

                Console.Write("User ID: ");
                int userId = Convert.ToInt32(Console.ReadLine());

                UserRepository usersRepository = new UserRepository("users.txt");

                User user = usersRepository.GetById(userId);
                if (user == null)
                {
                    Console.Clear();
                    Console.WriteLine("User not found.");
                    Console.ReadKey(true);
                    return;
                }

                Console.WriteLine("ID:" + user.Id);
                Console.WriteLine("Username :" + user.Username);
                Console.WriteLine("Password :" + user.Password);
                Console.WriteLine("Email :" + user.Email);
                Console.WriteLine("Display Name :" + user.DisplayName);
                Console.WriteLine("Is Admin:" + user.IsAdmin);

                Console.ReadKey(true);
            }

            private void Add()
            {
                Console.Clear();

                User user = new User();

                Console.WriteLine("Add new User:");

                Console.Write("Username: ");
                user.Username = Console.ReadLine();

                Console.Write("Password: ");
                user.Password = Console.ReadLine();

                Console.Write("Email: ");
                user.Email = Console.ReadLine();

                Console.Write("Display Name: ");
                user.DisplayName = Console.ReadLine();

                Console.Write("Is Admin (True/False): ");
                user.IsAdmin = Convert.ToBoolean(Console.ReadLine());

                UserRepository usersRepository = new UserRepository("users.txt");
                usersRepository.Save(user);

                Console.WriteLine("User saved successfully.");
                Console.ReadKey(true);
            }

            private void Update()
            {
                Console.Clear();

                Console.Write("User ID: ");
                int userId = Convert.ToInt32(Console.ReadLine());

                UserRepository usersRepository = new UserRepository("users.txt");
                User user = usersRepository.GetById(userId);

                if (user == null)
                {
                    Console.Clear();
                    Console.WriteLine("User not found.");
                    Console.ReadKey(true);
                    return;
                }

                Console.WriteLine("Editing User (" + user.Username + ")");
                Console.WriteLine("ID:" + user.Id);

                Console.WriteLine("Username :" + user.Username);
                Console.Write("New Username:");
                string username = Console.ReadLine();

                Console.WriteLine("Password :" + user.Password);
                Console.Write("New Password:");
                string password = Console.ReadLine();

                Console.WriteLine("Email :" + user.Email);
                Console.Write("New Email :");
                string email = Console.ReadLine();

                Console.WriteLine("Display Name :" + user.DisplayName);
                Console.Write("New Display Name:");
                string displayName = Console.ReadLine();

                Console.WriteLine("Is Admin :" + user.IsAdmin);
                Console.Write("New Is Admin (True/False):");
                string isAdmin = Console.ReadLine();


                if (!string.IsNullOrEmpty(username))
                    user.Username = username;
                if (!string.IsNullOrEmpty(password))
                    user.Password = password;
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                if (!string.IsNullOrEmpty(displayName))
                    user.DisplayName = displayName;
                if (!string.IsNullOrEmpty(isAdmin))
                    user.IsAdmin = Convert.ToBoolean(isAdmin);

                usersRepository.Save(user);

                Console.WriteLine("User saved successfully.");
                Console.ReadKey(true);
            }

            private void Delete()
            {
                UserRepository usersRepository = new UserRepository("users.txt");

                Console.Clear();

                Console.WriteLine("Delete User:");
                Console.Write("User Id: ");
                int userId = Convert.ToInt32(Console.ReadLine());

                User user = usersRepository.GetById(userId);
                if (user == null)
                {
                    Console.WriteLine("User not found!");
                }
                else
                {
                    usersRepository.Delete(user);
                    Console.WriteLine("User deleted successfully.");
                }
                Console.ReadKey(true);
            }
        }
    }

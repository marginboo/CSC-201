/* Simple guessing game in which the computer tries to guess the
   name of an animal the user has in mind by asking simple questions
   with yes/no answers.
   George Wells  --  23 September 2013 */

using cs2;
using System;
using System.IO;

public class Animal
  { private Tree<String> root;

    private void initTree ()
      { Tree<String> p;

        root = new Tree<String>("Does it live in water?");
        root.AddLeft(new Tree<String>("Does it have webbed feet?"));
        root.AddRight(new Tree<String>("Does it fly?"));

        p = root.Right();
        p.AddLeft(new Tree<String>("bird"));
        p.AddRight(new Tree<String>("Does it bark?"));
        p = p.Right();
        p.AddLeft(new Tree<String>("dog"));
        p.AddRight(new Tree<String>("cat"));

        // Return to left subtree of root
        p = root.Left();
        p.AddLeft(new Tree<String>("duck"));
        p.AddRight(new Tree<String>("fish"));
      } // initTree

    private bool answer ()
    // Get yes/no answer from user and return true for yes, false for no
      { String ans = " ";

        while (true)
          { try
              { ans = Console.ReadLine();
              }
            catch (IOException e)
              { Console.Error.WriteLine(e.StackTrace);
                System.Environment.Exit(1);
              }
            char ch = ans[0];
            if (ch == 'y' || ch == 'Y')
              return true;
            else
              if (ch == 'n' || ch == 'N')
                return false;
              else
                Console.WriteLine("Please answer yes or no.");
          }
      } // answer

    public void Play ()
      { Tree<String> pos;
        initTree();
        Console.WriteLine("Let's play guess the animal.");
        pos = root;
        while (pos != null)
          { if (pos.Left() != null) // Must be a question
              { Console.Write(pos.GetData());
                if (answer())
                  pos = pos.Left();
                else
                  pos = pos.Right();
              }
            else // Must be an answer
              { Console.WriteLine("It's a " + pos.GetData() + ".");
                Console.WriteLine("Is this the animal you had in mind");
                string ans = "";
                try
                {
                    ans = Console.ReadLine();
                }
                catch(IOException e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                    System.Environment.Exit(1);
                }
                char c = ans[0];
                if (c == 'y' || c == 'Y')
                    break;
                else
                {
                    if (c == 'n' || c == 'N')
                        Console.WriteLine("Please enter the name of your animal ");
                        string name = Console.ReadLine();
                        Console.WriteLine("How would you describe it");

                        pos.rightreplace(new Tree<string>(Console.ReadLine()));
                        pos.AddRight(new Tree<string>(name));
                        
                    
                }       

              }
          }
        if (pos == null)
            Console.WriteLine("Sorry, I don't know the animal you had in mind.");
      } // Play

    public static void Main (String[] args)
      { Animal a = new Animal();
        a.Play();
        Console.WriteLine("Do you wanna play again");
        
        a.Play();
        Console.ReadKey();
      } // Main

  } // class Animal

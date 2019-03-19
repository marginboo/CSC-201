// Program using a queue to perform a breadth-first maze search.
// George Wells  --  19 September 2013

using cs2;
using System;
using System.IO;

public class QSearch
  { private const int MAX_COORD = 10; // Size of maze

    private struct Position  // Coordinates of location in maze
      { public int r, // Row coordinate
                   c; // Column coordinate
      } // inner struct Position

    private IQueue<Position> posQueue = new ListQueue<Position>(); // Queue of positions still to be checked

    private bool[,] maze = new bool[MAX_COORD, MAX_COORD]; // Description of maze

    private bool[,] beenThere = new bool[MAX_COORD, MAX_COORD]; // Keep track of previous positions

    private void readMaze (String mazeFile)
      // Read in the maze
      { int r, c; // Loop counters
        TextReader f = File.OpenText(mazeFile);

        for (r = 0; r < MAX_COORD; r++)
          { String line = f.ReadLine();
            for (c = 0; c < MAX_COORD; c++)
              { maze[r,c] = (line[c] == ' ');
              }
          }
        f.Close();
      } // readMaze

    private void addPosition(int row, int col)
      // Put a new position on the queue of positions
      { Position p = new Position();
        p.r = row;
        p.c = col;
        posQueue.Add(p);
      } // addPosition

    public void SolveMaze ()
      { int r, c = 0; // Row and column coordinates;

        try
          { readMaze("D:\\JavaProgs\\cs2\\MAZE");
          }
        catch (IOException e)
          { Console.Error.WriteLine("Error reading data file");
            Console.Error.WriteLine(e.StackTrace);
            System.Environment.Exit(1);
          }

        // Initialise beenThere to all FALSE
        for (r = 0; r < MAX_COORD; r++)
          for (c = 0; c < MAX_COORD; c++)
            beenThere[r, c] = false;
        // Find starting position
        for (r = 0; r < MAX_COORD; r++)
          if (maze[r, 0]) // r is starting row
            break;
        // Put starting position on queue
        addPosition(r, 0);

        // Now do search
        while (! posQueue.IsEmpty())
          { Position nextPos;

            // Remove next position from queue and try all possible moves
            nextPos = posQueue.Remove();
            c = nextPos.c;
            r = nextPos.r;

            beenThere[r, c] = true; // Note that we have visited this spot
            Console.WriteLine("Visiting position: " + r + ", " + c);
            if (c == MAX_COORD-1) // Found exit, so leave search loop
              break;

            // Try to move up
            if (maze[r-1, c] && ! beenThere[r-1, c])
              addPosition(r-1, c);
            // Try to move right
            if (maze[r, c+1] && ! beenThere[r, c+1])
              addPosition(r, c+1);
            // Try to move down
            if (maze[r+1, c] && ! beenThere[r+1, c])
              addPosition(r+1, c);
            // Try to move left
            if (c > 0 && maze[r, c-1] && ! beenThere[r, c-1])
              addPosition(r, c-1);
          }// while

        if (c == MAX_COORD-1) // Found exit
          Console.WriteLine("Success!  Found exit at position: " + r + ", " + c);
        else
          Console.WriteLine("Failed to find exit from maze.");
      } // solveMaze

    public static void Main (String[] args)
      { QSearch qs = new QSearch();
        qs.SolveMaze();
      } // Main

  } // class QSearch

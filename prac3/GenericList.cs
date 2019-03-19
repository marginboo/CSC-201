using System;
using System.Text;
using System.Diagnostics;

namespace cs2
{
/// <summary>
/// Simple class to handle generic lists, using linked lists.
/// </summary>
/// <remarks>
/// Author: George Wells
/// Version: 1.0 (19 September 2013)
/// </remarks>
public class GenericList<T>
  { private class ListNode
      { public T data;
        public ListNode next;
      } // inner class ListNode

    /// <summary>Reference to the first ListNode in a GenericList.</summary>
    private ListNode first;

    /// <summary>Number of elements in a GenericList.</summary>
    private int numElements;

    /// <summary>
    /// Create an empty GenericList.
    /// <para><I>Postcondition:</I> The list is empty.</para>
    /// </summary>
    public GenericList ()
      { first = null;
        numElements = 0;
      } // GenericList constructor

    /// <summary>
    /// Place a new item at a specified position in a GenericList.
    /// <para><I>Precondition:</I> The position is positive or zero.</para>
    /// <para><I>Postcondition:</I> The object <c>item</c> appears at
    /// <c>position</c> in the list or at the end of the list if
    /// <c>position</c> is greater than the original length of the list.</para>
    /// </summary>
    /// <param name="item">The object to be added to the list.</param>
    /// <param name="position">The position in the list where the item should
    /// be added.</param>
    /// <exception cref="ArgumentException">if <c>position</c> is negative.</exception>
    public void Add (T item, int position)
      { if (position < 0)
          throw new ArgumentException("position is negative");
        ListNode node = new ListNode();
        node.data = item;
        ListNode curr = first,
                 prev = null;
        for (int k = 0; k < position && curr != null; k++) // Find position
          { prev = curr;
            curr = curr.next;
          }
        node.next = curr;
        if (prev != null)
          prev.next = node;
        else
          first = node;
        numElements++;
     } // Add

    /// <summary>Place a new item at the end of a GenericList.
    /// <para><I>Postcondition:</I> The object <c>item</c> appears at
    /// the end of the list.</para>
    /// </summary>
    /// <param name="item">The object to be added to the list.</param>
    public void Add (T item)
      { Add(item, numElements);
      } // Add

    /// <summary>
    /// Remove the item at a given position in a GenericList.
    /// <para><I>Precondition:</I> The position is that of a valid item.</para>
    /// <para><I>Postcondition:</I> The item at <c>position</c> has been
    ///  removed from the list.</para>
    /// </summary>
    /// <param name="position">The position of the item to be removed from the
    /// list.</param>
    /// <exception cref="IndexOutOfRangeException">if <c>position</c> is invalid.</exception>
    public void Remove (int position)
      { if (position < 0 || position >= numElements)
          throw new IndexOutOfRangeException("position is out of range");
        ListNode curr = first,
                 prev = null;
        for (int k = 0; curr != null && k < position; k++)
          { prev = curr;
            curr = curr.next;
          }
        Debug.Assert(curr != null);
        if (prev != null)
          prev.next = curr.next;
        else
          first = curr.next;
        numElements--;
      } // Remove

    /// <summary>
    /// Return the current number of elements in a GenericList.
    /// </summary>
    /// <returns>The number of elements in the list.</returns>
    public int Length ()
      { return numElements;
      } // Length

    /// <summary>
    /// Retrieve an element from an GenericList.
    /// <para><I>Precondition:</I> <c>index</c> is in range.</para>
    /// </summary>
    /// <param name="index">The position of the item to get from the list.</param>
    /// <returns>The element at position <c>index</c> in the list.</returns>
    /// <exception cref="System.IndexOutOfRangeException">if <c>index</c> is invalid.</exception>
    public T Get (int index)
      { if (index < 0 || index >= numElements) 
          throw new IndexOutOfRangeException("index is out of range");
        ListNode curr = first;
        for (int k = 0; curr != null && k < index; k++)
          curr = curr.next;
        Debug.Assert(curr != null);
        return curr.data;
      } // Get

    /// <summary>
    /// Change the value of an element in an GenericList.
    /// <para><I>Precondition:</I> <C>index</C> is in range.</para>
    /// </summary>
    /// <param name="index">The position of the item to be changed in the list.</param>
    /// <param name="item">The new value for the item.</param>
    /// <exception cref="IndexOutOfRangeException">if <c>index</c> is invalid.</exception>
    public void Set (int index, T item)
      { if (index < 0 || index >= numElements) 
          throw new IndexOutOfRangeException("index is out of range");
        ListNode curr = first;
        for (int k = 0; curr != null && k < index; k++)
          curr = curr.next;
        Debug.Assert(curr != null);
        curr.data = item;
      } // Set

    /// <summary>
    ///   Find item in a GenericList (uses the <c>.equals()</c> method for
    ///   comparisons).
    /// </summary>
    /// <param name="item">The item to be searched for.</param>
    /// <returns>The position of this item if it is found, otherwise -1.</returns>
    public int Position(T item)
      { ListNode curr = first;
        int k;
        for (k = 0; curr != null && !curr.data.Equals(item); k++, curr = curr.next)
          ; // Search for item in GenericList
        if (curr == null) // item was not found
          return -1;
        else
          return k;
      } // Position

    /// <summary>
    /// Return string representation of the GenericList.
    /// <para>The format is: <c>[ <I>item</I>, <I>item</I>, ... ]</c></para>
    /// </summary>
    /// <returns>A string representing the contents of this list.</returns>
    public override string ToString ()
    {
        StringBuilder s = new StringBuilder("[");
        for (ListNode curr = first; curr != null; curr = curr.next)
          { s.Append(curr.data.ToString());
            if (curr.next != null)
              s.Append(", ");
          }
        s.Append("]");
        return s.ToString();
      } // ToString

  } // class GenericList

} // namespace cs2
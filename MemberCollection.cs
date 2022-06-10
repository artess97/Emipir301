//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;


class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private Member[] members; //make sure members are sorted in dictionary order

    // Properties

    // get the capacity of this member colllection 
    // pre-condition: nil
    // post-condition: return the capacity of this member collection and this member collection remains unchanged
    public int Capacity { get { return capacity; } }

    // get the number of members in this member colllection 
    // pre-condition: nil
    // post-condition: return the number of members in this member collection and this member collection remains unchanged
    public int Number { get { return count; } }

   


    // Constructor - to create an object of member collection 
    // Pre-condition: capacity > 0
    // Post-condition: an object of this member collection class is created

    public MemberCollection(int capacity)
    {
        if (capacity > 0)
        {
            this.capacity = capacity;
            members = new Member[capacity];
            count = 0;
        }
    }

    // check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull()
    {
        return count == capacity;
    }

    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate will be added into this the member collection
    public void Add(IMember member)
    {
        if (!IsFull()) // Check if array is full
        {
            if (IsEmpty()) // Check if array is empty
            {
                members[count] = (Member)member;
                count++;
            }
            else
            {
                int isDuplicate = 0;

                for (int i = 0; i <= count - 1; i++) 
                {
                    if (member.CompareTo(members[i]) == 0) //Check duplicate
                    {
                        Console.WriteLine("Duplicate input.");
                        isDuplicate++;
                        break;
                    }
                }
                if (isDuplicate == 0) 
                {
                    int pos = count - 1;

                    while ((pos >= 0) && (members[pos].CompareTo(member) == 1)) // Sort the members according to their full names
                    {
                        members[pos + 1] = members[pos];
                        pos--;
                    }
                    members[pos + 1] = (Member)member; //Add member to the array
                    count++;
                }
            }
        }
        else 
        {
            Console.WriteLine("The list is full.");
        }
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
    public void Delete(IMember aMember)
    {
        if (Search(aMember))
        {
            int found = 0;
            for (int i = 0; i < count-1; i++) // Check all variables in the array
            {
                if (members[i].CompareTo(aMember) == 0 || found == 1)
                {
                    members[i] = members[i + 1]; // If found, remove it by shifting the array to the left
                    found = 1;
                }
            }
              count--;
        }
        else
        {
            Console.WriteLine("Not in the array.");
        }
    }

    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        int min = 0;
        int max = count - 1;
        
        while (min <= max) // Check all variables in the array
        {
            int divide = ((min + max) / 2);
            if (members[divide].CompareTo(member) == 0)
            {
                return true;
            }
            else if (members[divide].CompareTo(member) == 1)
            {
                max = divide -1;
            }
            else
            {
                min = divide + 1;
            }
        }
        return false;
    }

    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            this.members[i] = null;
        }
        count = 0;
    }

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public string ToString()
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s = s + members[i].ToString() + "\n";
        return s;
    }

    public IMember Find(IMember member)
    {
        int min = 0;
        int max = count - 1;

        while (min <= max) // Check all variables in the array
        {
            int divide = ((min + max) / 2);
            if (members[divide].CompareTo(member) == 0)
            {
                return members[divide];
            }
            else if (members[divide].CompareTo(member) == 1)
            {
                max = divide - 1;
            }
            else
            {
                min = divide + 1;
            }
        }
        return null;
    }
}


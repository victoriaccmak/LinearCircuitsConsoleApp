using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace LinearCircuits
{
    class Program
    {
        static private List<Node> nodes = new List<Node>();
        static private List<Branch> branches = new List<Branch>();

        static void Main(string[] args)
        {
            Node firstNode = new Node("A", 0, nodes);
            nodes.Add(firstNode);

            bool running = true;
            bool valid = false;
            int id = -1;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Choose an Option");
                Console.WriteLine("A) Add new node");
                Console.WriteLine("B) Add new branch");
                Console.WriteLine("C) Display results");
                Console.WriteLine("D) Display nodes");
                Console.WriteLine("E) Display branches");
                Console.WriteLine("F) Delete node");
                Console.WriteLine("G) Delete branch");
                Console.WriteLine("H) Exit");

                string option = Console.ReadLine().ToLower().Trim();
                Console.Clear();

                switch (option)
                {
                    case "a":
                        valid = false;

                        while (!valid)
                        {
                            Console.Write("Enter name of node: ");
                            option = Console.ReadLine().ToUpper().Trim();

                            valid = true;

                            for (int i = 0; i < nodes.Count; i++)
                            {
                                if (nodes[i].GetName().Equals(option))
                                {
                                    valid = false;
                                    Console.WriteLine("You already have a node named \"" + option + "\"");
                                }
                            }
                        }


                        AddNode(option);
                        break;

                    case "b":
                        Node pNode = new Node();
                        Node nNode = new Node();

                        valid = false;
                        while (!valid)
                        {
                            Console.WriteLine("Enter positive (or tip of current) node: ");
                            option = Console.ReadLine().ToUpper().Trim();

                            for (int i = 0; i < nodes.Count; i++)
                            {
                                if (nodes[i].GetName().Equals(option))
                                {
                                    pNode = nodes[i];
                                    valid = true;
                                    break;
                                }
                            }
                        }

                        valid = false;
                        while (!valid)
                        {
                            Console.WriteLine("Enter negative (or tail of current) node: ");
                            option = Console.ReadLine().ToUpper().Trim();

                            for (int i = 0; i < nodes.Count; i++)
                            {
                                if (nodes[i].GetName().Equals(option))
                                {
                                    nNode = nodes[i];
                                    valid = true;
                                    break;
                                }
                            }
                        }

                        valid = false;
                        
                        while (!valid)
                        {
                            Console.WriteLine("Enter type of branch: ");
                            Console.WriteLine("A) Resistor");
                            Console.WriteLine("B) Voltage Source");
                            Console.WriteLine("C) Current Source");

                            if (branches.Count != 0)
                            {
                                Console.WriteLine("D) VCCS");
                                Console.WriteLine("E) VCVS");
                                Console.WriteLine("F) CCCS");
                                Console.WriteLine("G) CCVS");
                            }

                            Console.WriteLine("H) Don't Add Anything");

                            option = Console.ReadLine().ToLower().Trim();
                            valid = true;
                            bool valueValid = false;

                            float temp1 = 0;
                            int temp2 = 0;
                            
                            if (branches.Count == 0 && (option.Equals("d") || option.Equals("e") || option.Equals("f") || option.Equals("g")))
                            {
                                option = "";
                            }

                            switch (option)
                            {
                                case "a":
                                    while (!valueValid)
                                    {
                                        Console.Write("Enter Resistance: ");
                                        valueValid = float.TryParse(Console.ReadLine(), out temp1);
                                        valueValid = temp1 != 0;
                                        Console.WriteLine("Hint: if your resistance is 0, the 2 nodes are actually the same node");
                                    }

                                    branches.Add(new Resistor(branches.Count, temp1, nNode, pNode));
                                    nNode.AddBranch(branches[branches.Count - 1]);
                                    pNode.AddBranch(branches[branches.Count - 1]);
                                    break;

                                case "b":
                                    while (!valueValid)
                                    {
                                        Console.Write("Enter Voltage: ");
                                        valueValid = float.TryParse(Console.ReadLine(), out temp1);
                                        valueValid = temp1 != 0;
                                        Console.WriteLine("Hint: if your voltage is 0, the 2 nodes are actually the same node");
                                    }

                                    branches.Add(new VSource(branches.Count, temp1, nNode, pNode));
                                    nNode.AddBranch(branches[branches.Count - 1]);
                                    pNode.AddBranch(branches[branches.Count - 1]);
                                    break;

                                case "c":
                                    while (!valueValid)
                                    {
                                        Console.Write("Enter Current: ");
                                        valueValid = float.TryParse(Console.ReadLine(), out temp1);
                                    }

                                    branches.Add(new ISource(branches.Count, temp1, nNode, pNode));
                                    nNode.AddBranch(branches[branches.Count - 1]);
                                    pNode.AddBranch(branches[branches.Count - 1]);
                                    break;

                                case "d":
                                    Console.Write("Enter multiplier: ");
                                    temp1 = float.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    DisplayBranches();
                                    Console.Write("Enter branch ID: ");
                                    temp2 = Int32.Parse(Console.ReadLine());
                                    branches.Add(new VCCS(branches.Count, temp1, branches[temp2], nNode, pNode));
                                    nNode.AddBranch(branches[branches.Count - 1]);
                                    pNode.AddBranch(branches[branches.Count - 1]);
                                    break;

                                case "e":
                                    Console.Write("Enter multiplier: ");
                                    temp1 = float.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    DisplayBranches();
                                    Console.Write("Enter branch ID: ");
                                    temp2 = Int32.Parse(Console.ReadLine());
                                    branches.Add(new VCVS(branches.Count, temp1, branches[temp2], nNode, pNode));
                                    nNode.AddBranch(branches[branches.Count - 1]);
                                    pNode.AddBranch(branches[branches.Count - 1]);
                                    break;

                                case "f":
                                    Console.Write("Enter multiplier: ");
                                    temp1 = float.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    DisplayBranches();
                                    Console.Write("Enter branch ID: ");
                                    temp2 = Int32.Parse(Console.ReadLine());
                                    branches.Add(new CCCS(branches.Count, temp1, branches[temp2], nNode, pNode));
                                    nNode.AddBranch(branches[branches.Count - 1]);
                                    pNode.AddBranch(branches[branches.Count - 1]);
                                    break;

                                case "g":
                                    Console.Write("Enter multiplier: ");
                                    temp1 = float.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    DisplayBranches();
                                    Console.Write("Enter branch ID: ");
                                    temp2 = Int32.Parse(Console.ReadLine());
                                    branches.Add(new CCVS(branches.Count, temp1, branches[temp2], nNode, pNode));
                                    nNode.AddBranch(branches[branches.Count - 1]);
                                    pNode.AddBranch(branches[branches.Count - 1]);
                                    break;

                                case "h":
                                    Console.WriteLine("Cancelled");
                                    break;

                                default:
                                    Console.WriteLine("Not a valid option. Try again");
                                    valid = false;
                                    break;
                            }
                        }
                        
                        break;

                    case "c":
                        valid = false;
                        Node ground = null;

                        while (!valid)
                        {
                            Console.WriteLine("Choose your ground node: ");
                            option = Console.ReadLine().ToUpper();
                            for (int i = 0; i < nodes.Count; i++)
                            {
                                if (nodes[i].GetName().ToUpper().Equals(option))
                                {
                                    ground = nodes[i];

                                    if (!ground.IsConnected())
                                    {
                                        Console.WriteLine("Your ground node must have at least 1 branch");
                                    }
                                    else
                                    {
                                        valid = true;
                                    }
                                    break;
                                }
                            }
                        }

                        CalcNodalVoltages(ground);
                        CalcCurrents();
                        Console.WriteLine();
                        Console.WriteLine("Nodal Analysis Results");
                        for (int i = 0; i < nodes.Count; i++)
                        {
                            Console.WriteLine(nodes[i].GetId().ToString().PadRight(10) + nodes[i].GetName().PadRight(10) + Math.Round(nodes[i].GetV(), 4) + "V");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Current through each branch");
                        for (int i = 0; i < branches.Count; i++)
                        {
                            if (branches[i] is ISource || branches[i] is CCCS || branches[i] is VCCS)
                            {
                                Console.WriteLine("Branch #" + branches[i].GetId().ToString().PadRight(10) + Math.Round(branches[i].GetI(), 4) + "A from node " + branches[i].GetN().GetName() + " to node " + branches[i].GetP().GetName());
                            }
                            else
                            {
                                Console.WriteLine("Branch #" + branches[i].GetId().ToString().PadRight(10) + Math.Round(branches[i].GetI(), 4) + "A from node " + branches[i].GetP().GetName() + " to node " + branches[i].GetN().GetName());
                            }
                        }
                        break;

                    case "d":
                        Console.WriteLine("Nodes");
                        DisplayNodes();
                        break;

                    case "e":
                        Console.WriteLine("Branches");
                        DisplayBranches();
                        break;

                    case "f":
                        Console.WriteLine("Nodes");
                        DisplayNodes();
                        Console.WriteLine("You will delete the node and all branches connected to the node");

                        valid = false;
                        id = -1;

                        while (!valid)
                        {
                            Console.WriteLine();
                            Console.Write("Node to delete (Enter ID): ");
                            valid = Int32.TryParse(Console.ReadLine(), out id) && id < nodes.Count && id >= 0;
                        }

                        RemoveNode(id);
                        break;

                    case "g":
                        Console.WriteLine("Branches");
                        DisplayBranches();

                        valid = false;
                        id = -1;

                        while (!valid)
                        {
                            Console.WriteLine();
                            Console.Write("Branch to delete (Enter ID): ");
                            valid = Int32.TryParse(Console.ReadLine(), out id) && id < branches.Count && id >= 0;
                        }

                        RemoveBranch(id);
                        break;

                    case "h":
                        Console.WriteLine("You've stopped your simulation");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again");
                        break;
                }

                Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();
            }
        }

        public static void AddNode(string name)
        {
            Node temp = new Node(name, nodes.Count, nodes);

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].AddNewNode();
            }

            nodes.Add(temp);
        }

        public static void DisplayBranches()
        {
            for (int i = 0;i < branches.Count; i++)
            {
                Console.WriteLine(branches[i].GetDesc());
            }
        }
        public static void DisplayNodes()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Display();
            }
        }

        public static void CalcNodalVoltages(Node ground)
        {
            float[][] matrix = new float[nodes.Count][];

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new float[nodes.Count + 1];
            }

            for (int i = 0; i < branches.Count; i++)
            {
                if (branches[i] is Resistor)
                {
                    if (!branches[i].GetP().GetHasVSource())
                    {
                        matrix[branches[i].GetP().GetId()][branches[i].GetP().GetId()] += 1f / branches[i].GetR();
                        matrix[branches[i].GetP().GetId()][branches[i].GetN().GetId()] -= 1f / branches[i].GetR();
                    }

                    if (!branches[i].GetN().GetHasVSource())
                    {
                        matrix[branches[i].GetN().GetId()][branches[i].GetN().GetId()] += 1f / branches[i].GetR();
                        matrix[branches[i].GetN().GetId()][branches[i].GetP().GetId()] -= 1f / branches[i].GetR();
                    }
                }
                else if (branches[i] is ISource)
                {
                    if (!branches[i].GetP().GetHasVSource())
                    {
                        matrix[branches[i].GetP().GetId()][nodes.Count] += branches[i].GetI();
                    }

                    if (!branches[i].GetN().GetHasVSource())
                    {
                        matrix[branches[i].GetN().GetId()][nodes.Count] -= branches[i].GetI();
                    }
                }
                else if (branches[i] is VSource)
                {
                    if (branches[i].GetP().GetVSourceNum() == branches[i].GetId())
                    {
                        matrix[branches[i].GetP().GetId()][branches[i].GetP().GetId()] = 1;
                        matrix[branches[i].GetP().GetId()][branches[i].GetN().GetId()] = -1;
                        matrix[branches[i].GetP().GetId()][nodes.Count] = branches[i].GetV();
                    }
                    else if (branches[i].GetN().GetVSourceNum() == branches[i].GetId())
                    {
                        matrix[branches[i].GetN().GetId()][branches[i].GetP().GetId()] = 1;
                        matrix[branches[i].GetN().GetId()][branches[i].GetN().GetId()] = -1;
                        matrix[branches[i].GetN().GetId()][nodes.Count] = branches[i].GetV();
                    }
                }
                else if (branches[i] is VCVS)
                {
                    if (branches[i].GetP().GetVSourceNum() == branches[i].GetId())
                    {
                        matrix[branches[i].GetP().GetId()][branches[i].GetP().GetId()] = 1;
                        matrix[branches[i].GetP().GetId()][branches[i].GetN().GetId()] = -1;
                        matrix[branches[i].GetP().GetId()][branches[i].GetControlBranch().GetP().GetId()] -= branches[i].GetMult();
                        matrix[branches[i].GetP().GetId()][branches[i].GetControlBranch().GetN().GetId()] += branches[i].GetMult();
                    }
                    else if (branches[i].GetN().GetVSourceNum() == branches[i].GetId())
                    {
                        matrix[branches[i].GetN().GetId()][branches[i].GetP().GetId()] = 1;
                        matrix[branches[i].GetN().GetId()][branches[i].GetN().GetId()] = -1;
                        matrix[branches[i].GetN().GetId()][branches[i].GetControlBranch().GetP().GetId()] -= branches[i].GetMult();
                        matrix[branches[i].GetN().GetId()][branches[i].GetControlBranch().GetN().GetId()] += branches[i].GetMult();
                    }
                }
                else if (branches[i] is CCVS)
                {
                    if (branches[i].GetP().GetVSourceNum() == branches[i].GetId())
                    {
                        matrix[branches[i].GetP().GetId()][branches[i].GetP().GetId()] = 1;
                        matrix[branches[i].GetP().GetId()][branches[i].GetN().GetId()] = -1;
                        
                        if (branches[i].GetControlBranch() is ISource)
                        {
                            matrix[branches[i].GetP().GetId()][nodes.Count] = branches[i].GetMult() * branches[i].GetControlBranch().GetI();
                        }
                        else if (branches[i].GetControlBranch() is Resistor)
                        {
                            matrix[branches[i].GetP().GetId()][branches[i].GetControlBranch().GetP().GetId()] -= branches[i].GetMult() / branches[i].GetControlBranch().GetR();
                            matrix[branches[i].GetP().GetId()][branches[i].GetControlBranch().GetN().GetId()] += branches[i].GetMult() / branches[i].GetControlBranch().GetR();
                        }
                    }
                    else if (branches[i].GetN().GetVSourceNum() == branches[i].GetId())
                    {
                        matrix[branches[i].GetN().GetId()][branches[i].GetP().GetId()] = 1;
                        matrix[branches[i].GetN().GetId()][branches[i].GetN().GetId()] = -1;

                        if (branches[i].GetControlBranch() is ISource)
                        {
                            matrix[branches[i].GetN().GetId()][nodes.Count] = branches[i].GetMult() * branches[i].GetControlBranch().GetI();
                        }
                        else if (branches[i].GetControlBranch() is Resistor)
                        {
                            matrix[branches[i].GetN().GetId()][branches[i].GetControlBranch().GetP().GetId()] -= branches[i].GetMult() / branches[i].GetControlBranch().GetR();
                            matrix[branches[i].GetN().GetId()][branches[i].GetControlBranch().GetN().GetId()] += branches[i].GetMult() / branches[i].GetControlBranch().GetR();
                        }
                    }
                }
                else if (branches[i] is VCCS)
                {
                    if (!branches[i].GetP().GetHasVSource())
                    {
                        matrix[branches[i].GetP().GetId()][branches[i].GetControlBranch().GetP().GetId()] -= branches[i].GetMult();
                        matrix[branches[i].GetP().GetId()][branches[i].GetControlBranch().GetN().GetId()] += branches[i].GetMult();
                    }

                    if (!branches[i].GetN().GetHasVSource())
                    {
                        matrix[branches[i].GetN().GetId()][branches[i].GetControlBranch().GetP().GetId()] += branches[i].GetMult();
                        matrix[branches[i].GetN().GetId()][branches[i].GetControlBranch().GetN().GetId()] -= branches[i].GetMult();
                    }
                }
                else if (branches[i] is CCCS)
                {
                    if (!branches[i].GetP().GetHasVSource())
                    {
                        if (branches[i].GetControlBranch() is ISource)
                        {
                            matrix[branches[i].GetP().GetId()][nodes.Count] = branches[i].GetMult() * branches[i].GetControlBranch().GetI();
                        }
                        else if (branches[i].GetControlBranch() is Resistor)
                        {
                            matrix[branches[i].GetP().GetId()][branches[i].GetControlBranch().GetP().GetId()] -= branches[i].GetMult() / branches[i].GetControlBranch().GetR();
                            matrix[branches[i].GetP().GetId()][branches[i].GetControlBranch().GetN().GetId()] += branches[i].GetMult() / branches[i].GetControlBranch().GetR();
                        }
                    }

                    if (!branches[i].GetN().GetHasVSource())
                    {
                        if (branches[i].GetControlBranch() is ISource)
                        {
                            matrix[branches[i].GetN().GetId()][nodes.Count] = -branches[i].GetMult() * branches[i].GetControlBranch().GetI();
                        }
                        else if (branches[i].GetControlBranch() is Resistor)
                        {
                            matrix[branches[i].GetN().GetId()][branches[i].GetControlBranch().GetP().GetId()] += branches[i].GetMult() / branches[i].GetControlBranch().GetR();
                            matrix[branches[i].GetN().GetId()][branches[i].GetControlBranch().GetN().GetId()] -= branches[i].GetMult() / branches[i].GetControlBranch().GetR();
                        }
                    }
                }
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i][ground.GetId()] = 0;
            }

            DisplayMatrix(matrix);
            Console.WriteLine("This is your matrix. Press ENTER to simplify.");
            Console.ReadLine();

            matrix = SolveMatrix(matrix, ground.GetId());
            ground.SetV(0);

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length - 1; j++)
                {
                    if (matrix[i][j] != 0)
                    {
                        nodes[j].SetV(matrix[i][matrix[i].Length - 1]);
                        break;
                    }
                }
            }
        }

        //Has Issues
        public static float[][] SolveMatrix(float[][] matrix, int gndId)
        {
            if (matrix.Length > 0)
            {
                for (int i = 0; i < matrix[0].Length - 1; i++)
                {
                    bool zeroCol = true;
                    int row = 0;
                    for ( ; row < matrix.Length; row++)
                    {
                        if (matrix[row][i] != 0)
                        {
                            bool leadingVal = true;

                            for (int j = i - 1; j >= 0; j--)
                            {
                                if (matrix[row][j] != 0)
                                {
                                    leadingVal = false;
                                }
                            }

                            if (leadingVal)
                            {
                                zeroCol = false;
                                break;
                            }
                        }
                    }

                    if (!zeroCol)
                    {
                        Console.WriteLine("row: " + row);

                        //Multiply the row by the reciprocal of the ith, ith entry to make the ith, ith entry equal to 1
                        float recip = 1f / matrix[row][i];

                        Console.WriteLine("Multiply row " + row + " by " + Math.Round(recip, 4));

                        for (int j = 0; j < matrix[i].Length; j++)
                        {
                            matrix[row][j] *= recip;
                        }

                        DisplayMatrix(matrix);
                        Console.WriteLine();

                        //Make all other entries in the ith column equal to 0
                        for (int j = 0; j < matrix.Length; j++)
                        {
                            if (j != row)
                            {
                                float mult = matrix[j][i];
                                Console.WriteLine("Subtract " + mult + " R" + row + " from row " + j);

                                for (int k = 0; k < matrix[j].Length; k++)
                                {
                                    matrix[j][k] = matrix[j][k] - matrix[row][k] * mult;
                                }

                                DisplayMatrix(matrix);
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
            
            return matrix;
        }

        public static void DisplayMatrix(float[][] matrix)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Console.Write(nodes[i].GetName().PadRight(10));
            }

            Console.WriteLine();

            for (int i = 0; i < matrix.Length; i++) 
            { 
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write(Math.Round(matrix[i][j], 2).ToString().PadRight(10));
                }

                Console.WriteLine();
            }
        }

        public static void CalcCurrents()
        {
            for (int i = 0; i < branches.Count; i++)
            {
                branches[i].CalcI();
            }
        }

        public static void RemoveBranch(int id)
        {
            branches.RemoveAt(id);

            for (int i = id; i < branches.Count; i++)
            {
                branches[i].SetId(i);
            }
        }

        public static void RemoveNode(int id)
        {
            for (int i = 0; i < branches.Count; i++)
            {
                if (branches[i].GetP().GetId() == id || branches[i].GetN().GetId() == id)
                {
                    RemoveBranch(i);
                    i--;
                }
            }

            nodes.RemoveAt(id);

            //Shift ID
            for (int i = id; i < nodes.Count; i++)
            {
                nodes[i].SetId(i);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].UpdateDeletedNode(id);
            }
        }
    }
}

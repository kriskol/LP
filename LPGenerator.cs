using System;
using System.Text;
using System.Collections.Generic;

namespace LPPraktickaUloha
{
    /// <summary>
    /// Class that represents a graph.
    /// </summary>
    class Graph
    {
        private uint numberOfVertices;
        public uint NumberOfVertices { get { return numberOfVertices; } }
        List<Edge>[] edges;
        public List<Edge>[] Edges { get { return edges; } }
        
        
        public void AddEdge(Edge edge)
        {
            edges[edge.from].Add(edge);
        }
        public Graph(uint numberOfVertices)
        {
            this.numberOfVertices = numberOfVertices;
            edges = new List<Edge>[numberOfVertices];

            for (int i = 0; i < numberOfVertices; i++)
                edges[i] = new List<Edge>();
        }
    }
    
    /// <summary>
    /// Struct used to represent an edge in graph.
    /// </summary>
    struct Edge:IEquatable<Edge>
    {
        public uint from;
        public uint to;
        public uint weight;
                
        public Edge(uint from, uint to, uint weight = 0)
        {
            this.from = from;
            this.to = to;
            this.weight = weight;
        }

        public bool Equals(Edge other)
        {
            if (other.from == from && other.to == to && other.weight == weight)
                return true;

            return false;
        }
    }
    
    /// <summary>
    /// Class used to process input from standard input.
    /// </summary>
    class ProcessInput
    {          
        /// <summary>
        /// Creates edge from given string representation of edge.
        /// </summary>
        /// <returns>The edge unweighted.</returns>
        /// <param name="edgeStringSplittedFormat">Edge string splitted format.</param>
        private static Edge CreateEdge(string[] edgeStringSplittedFormat)
        {
            uint weight = 0;

            if (edgeStringSplittedFormat.Length == 3)
                weight = uint.Parse(edgeStringSplittedFormat[2]);
            
            return new Edge(uint.Parse(edgeStringSplittedFormat[0]), uint.Parse(edgeStringSplittedFormat[1]), weight);
        }
        /// <summary>
        /// Method adds edges from standard input to given representation of a graph.
        /// </summary>
        /// <param name="graph">Graph.</param>
        /// <param name="numberOfEdges">Number of edges.</param>
        private static void AddEdgesToGraph(Graph graph, uint numberOfEdges)
        {
            string edgeStringFormat;
            string[] edgeStringSplittedFormat;
            
            for(int i = 0; i < numberOfEdges; i++)
            {
                edgeStringFormat = Console.ReadLine();
                edgeStringSplittedFormat = edgeStringFormat.Split(new char[] { ' ', '\t', '-', '>', '(',')' }, 
                                                                  StringSplitOptions.RemoveEmptyEntries);

                graph.AddEdge(CreateEdge(edgeStringSplittedFormat));
            }
        }
        /// <summary>
        /// Creates new graph based on the data acquired from the input.
        /// </summary>
        /// <returns>The graph.</returns>
        /// <param name="numberOfVertices">Number of vertices.</param>
        /// <param name="numberOfEdges">Number of edges.</param>
        private static Graph CreateGraph(uint numberOfVertices, uint numberOfEdges)
        {
            Graph graph = new Graph(numberOfVertices);
            AddEdgesToGraph(graph, numberOfEdges);

            return graph;
        }
        /// <summary>
        /// Processes the first line of the first assigment. Thus gains and returns number of edges and vertices
        /// of the graph from the assigment.
        /// </summary>
        /// <param name="numberOfVertices">Number of vertices.</param>
        /// <param name="numberOfEdges">Number of edges.</param>
        private static void ProcessFirstLineFirstAssigment(out uint numberOfVertices, out uint numberOfEdges)
        {
            string firstLine = Console.ReadLine();
            string[] firstLineSplitted = firstLine.Split(new char[] { ' ', ':', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            numberOfVertices = uint.Parse(firstLineSplitted[1]);
            numberOfEdges = uint.Parse(firstLineSplitted[2]);
        }
        /// <summary>
        /// Processes the first line of the second assigment. Thus gains and returns number of edges and vertices
        /// of the graph from the assigment.
        /// </summary>
        /// <param name="numberOfVertices">Number of vertices.</param>
        /// <param name="numberOfEdges">Number of edges.</param>
        private static void ProcessFirstLineSecondAssigment(out uint numberOfVertices, out uint numberOfEdges)
        {
            string firstLine = Console.ReadLine();
            string[] firstLineSplitted = firstLine.Split(new char[] { ' ', ':', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            numberOfVertices = uint.Parse(firstLineSplitted[2]);
            numberOfEdges = uint.Parse(firstLineSplitted[3]);
        }
        /// <summary>
        /// Method creates graph described by data from standard input. Expects data in input format
        /// of the first assigment.
        /// </summary>
        /// <returns>The to graph.</returns>
        public static Graph InputToGraphFirstAssigment()
        {
            uint numberOfVertices;
            uint numberOfEdges;
            ProcessFirstLineFirstAssigment(out numberOfVertices, out numberOfEdges);
                   
            return CreateGraph(numberOfVertices, numberOfEdges);
        }
        /// <summary>
        /// Method creates graph described by data from standard input. Expects data in input format
        /// of the second assigment.
        /// </summary>
        /// <returns>The to graph second assigment.</returns>
        public static Graph InputToGraphSecondAssigment()
        {
            uint numberOfVertices;
            uint numberOfEdges;
            ProcessFirstLineSecondAssigment(out numberOfVertices,out numberOfEdges);

            return CreateGraph(numberOfVertices, numberOfEdges);  
        }
    }
    
    /// <summary>
    /// Class used for creating and also printing to the standard output LP for given assigments based on graph
    /// that was acquired from the standard input.
    /// </summary>
    class CreateLP
    {
        /// <summary>
        /// Finds out whether given "edge" ends in vertex "to". 
        /// </summary>
        /// <returns><c>true</c>, if edge to was ised, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge.</param>
        /// <param name="to">To.</param>
        private static bool IsEdgeTo(Edge edge, uint to)
        {
            if (edge.to == to)
                return true;

            return false; 
        }
        /// <summary>
        /// Creates and prints to standard output LP for the first assigment.
        /// </summary>
        /// <param name="graph">Graph.</param>
        public static void CreatePrintLPFirstAssigment(Graph graph)
        {
            Console.WriteLine("param N := " + graph.NumberOfVertices+";");
            Console.WriteLine("set Vertices := (0..N-1);");
            Console.WriteLine("var M >= 0;");
            Console.WriteLine("var x{i in Vertices} >= -1;");
            Console.WriteLine("minimize obj: M;");
            Console.WriteLine("condition_vertex{i in Vertices}: x[i] <= M;");
            
            foreach (List<Edge> vertexEdges in graph.Edges)
                foreach (Edge edge in vertexEdges)
                    Console.WriteLine("condition_edge{0}_{1}: x[{0}] + 1 <= x[{1}];", edge.from, edge.to);

            Console.WriteLine("solve;");
            Console.WriteLine("printf \"#OUTPUT: %d\\n\",M;");
            Console.WriteLine("printf{i in Vertices} \"v_%d: %d\\n\",i,x[i];");
            Console.WriteLine("printf \"#OUTPUT END\\n\";");
            Console.WriteLine("end;");
        }
        /// <summary>
        /// Created and prints to standard output LP for the second assigment.
        /// </summary>
        /// <param name="graph">Graph.</param>
        public static void CreatPrintLPSecondAssigment(Graph graph)
        {
            StringBuilder setEdges = new StringBuilder();
            setEdges.Append("set Edges := {");

            for(int i = 0; i < graph.NumberOfVertices; i++)
                foreach (Edge edge in graph.Edges[i])
                {
                    setEdges.Append('(');
                    setEdges.Append(edge.from);
                    setEdges.Append(',');
                    setEdges.Append(edge.to);
                    setEdges.Append(')');
                    setEdges.Append(',');
                }
            if(graph.Edges.Length != 0)
                setEdges.Remove(setEdges.Length - 1, 1);
                
            setEdges.Append("};");

            Console.WriteLine(setEdges);
            Console.WriteLine("var edge{(i,j) in Edges}, >= 0,  <= 1, integer;");
            
            StringBuilder minimalizedFunction = new StringBuilder();
            minimalizedFunction.Append("minimize min: ");

            for (int i = 0; i < graph.NumberOfVertices; i++)
                foreach (Edge edge in graph.Edges[i])
                {
                    minimalizedFunction.Append("edge[");
                    minimalizedFunction.Append(edge.from);
                    minimalizedFunction.Append(',');
                    minimalizedFunction.Append(edge.to);
                    minimalizedFunction.Append("] * ");
                    minimalizedFunction.Append(edge.weight);
                    minimalizedFunction.Append(" +");
                }
            
            minimalizedFunction.Append(" 0;");

            Console.WriteLine(minimalizedFunction);

            for (int i = 0; i < graph.NumberOfVertices; i++)
                foreach (Edge edge in graph.Edges[i])
                    foreach (Edge edgeNext in graph.Edges[edge.to])
                        foreach (Edge edgeNextNext in graph.Edges[edgeNext.to])
                            if (edgeNextNext.to == edge.from)
                                Console.WriteLine("condition_edge{0}_{1}_{2}: edge[{0},{1}] + edge[{1},{2}] + edge[{2},{0}] >= 1;",
                                                    edge.from, edge.to, edgeNext.to);
                            else if (graph.Edges[edgeNextNext.to].Exists(e => IsEdgeTo(e, edge.from)))
                                Console.WriteLine("condition_edge{0}_{1}_{2}_{3}: edge[{0},{1}] + edge[{1},{2}] + edge[{2},{3}] + edge[{3},{0}] >= 1;", 
                                                   edge.from,edge.to, edgeNext.to, edgeNextNext.to);

            Console.WriteLine("solve;");
            Console.WriteLine("printf \"#OUTPUT: %d\\n\",min;");
            Console.WriteLine("printf{(i,j) in Edges}(if edge[i,j] > 0 then \"%d --> %d\\n\" else \"\"),i,j;");
            Console.WriteLine("printf \"#OUTPUTEND END\\n\";");
            Console.WriteLine("end;");
            
        }      
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Unsufficient number of arguments was set. There has to be set one arg, \"first\"" +
                                   "for creating LP for the first assigment or \"second\" for creating LP" +
                                   "for the second assigment.");
            }

            //For first assigment
            if (args[0] == "first")
            {
                Graph graph = ProcessInput.InputToGraphFirstAssigment();
                CreateLP.CreatePrintLPFirstAssigment(graph);
            }
            //For second assigment.
            else if(args[0] == "second")
            {
                Graph graph = ProcessInput.InputToGraphSecondAssigment();
                CreateLP.CreatPrintLPSecondAssigment(graph);
            }
        }
    }
}

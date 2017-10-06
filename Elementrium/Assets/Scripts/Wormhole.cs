using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using BackpackObject;
using TriumObject;
using System;

namespace WormholeObject {
    
    public class Wormhole : MonoBehaviour
    {

        public float speed = 5f;

		private int rangeStart;         // The beginning of the range
		private int rangeEnd;           // The end of the range
										// private int wormholeLevel;      // The level of the wormhole
		private Backpack bp;            // the backpack of the user

		private int rangeMax = 92;
		private int rangeMin = 1;

		public Wormhole(Backpack bp)
		{
			this.bp = bp;
			rangeStart = 1;
			rangeEnd = 1;
			//    wormholeLevel = 0;
		}


		public int getStart()
		{
			return this.rangeStart;
		}

		public int getEnd()
		{
			return this.rangeEnd;
		}

		public void increaseStart()
		{
			rangeStart = (rangeStart == rangeMax) ? rangeStart : rangeStart++;
		}

		public void decreaseStart()
		{
			rangeStart = (rangeStart == rangeMin) ? rangeStart : rangeStart--;
		}

		public void increaseEnd()
		{
			rangeEnd = (rangeEnd == rangeMax) ? rangeEnd : rangeEnd++;
		}

		public void decreaseEnd()
		{
			rangeEnd = (rangeEnd == rangeMin) ? rangeEnd : rangeEnd--;
		}

		public void increaseRange()
		{
			if (rangeEnd == rangeMax)
			{
				return;
			}

			rangeStart++;
			rangeEnd++;
		}

		public void decreaseRange()
		{
			if (rangeStart == 1)
			{
				return;
			}

			rangeStart--;
			rangeEnd--;
		}

		/**
         * Algorithm to generate atoms and add them to the backpack.
         * 
         * Will generate an atom within the range specified by the wormhole,
         * and increases the corresponding Trium's count within the backpack.
         * 
         */
		private void generateAtoms()
		{

			validateRange();

			System.Random rnd = new System.Random();

			int atom = rnd.Next(rangeStart, rangeEnd + 1);

			/******** BEGIN DATABASE SECTION ********/

			// TODO: SIMPLIFY IN SPRINT 2

			string query = "SELECT t.ID, t.Name, e.AtomicNumber " +
				"FROM Trium t" +
				"INNER JOIN Element e ON e.ID = ISNULL(t.ElementID, -1)" +
				"WHERE ISNULL(e.AtomicNumber, -1) = " + atom;

			// Create new connection to database
			string connection = "URI = file:" + Application.dataPath + "/Elementrium.db";
			IDbConnection dbConn = (IDbConnection)new SqliteConnection(connection);

			// Open the connection
			dbConn.Open();

			// Set up new command query
			IDbCommand dbCmd = dbConn.CreateCommand();
			dbCmd.CommandText = query;

			// Execute query
			IDataReader dbReader = dbCmd.ExecuteReader();

			int rowID = -1;
			string name = "none";
			int AtomicNumber = -1;

			// Read dataset result
			while (dbReader.Read())
			{

				rowID = dbReader.GetInt32(0);
				name = dbReader.GetString(1);
				AtomicNumber = dbReader.GetInt32(2);

				break;
			}


			if (rowID == -1 || name == "none" || AtomicNumber == -1)
			{
				return;
			}

			// Add newly generated atom to Backpack
			bp.addToBackpack(rowID, name, AtomicNumber);

			/********** TODO: CREATE BUDDY OBJECT HERE **********/
			/**********      END DATABASE SECTION      **********/

			return;




		}

		/**
         * 
         * Validates the rangeStart and rangeEnd.
         * 
         * If any issues occur, the range will start at 1 and go up to 4 or
         * the next highest unlocked atom
         *
         */
		private void validateRange()
		{

			bool fixRange = false;

			if (rangeStart < rangeMin || rangeEnd > rangeMax)
			{
				fixRange = true;
			}

			if (rangeEnd < rangeStart || Math.Abs(rangeEnd - rangeStart) > 3)
			{
				fixRange = true;
			}

			// TODO: ADD OUT OF UNLOCKED-BOUNDS CASE once level system enabled


			if (!fixRange)
			{
				return;
			}

			// Reset the range start to 1 (Hydrogen)
			rangeStart = rangeMin;

			// TODO: CHECK BY LEVEL INSTEAD OF BY HASHMAP/TABLE ID
			if (bp.getBP().Contains(4))
			{
				rangeEnd = 4;
			}
			else if (bp.getBP().Contains(3))
			{
				rangeEnd = 3;
			}
			else if (bp.getBP().Contains(2))
			{
				rangeEnd = 2;
			}
			else
			{
				rangeEnd = 1;
			}


		}


		public void onTouchMethod()
		{
			// STUFF RELATED TO TAPPING THE WORMHOLE
			// Waiting for Vinson to coordinate this
			generateAtoms();
		}

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 0, speed * -1);
        }
    }
}
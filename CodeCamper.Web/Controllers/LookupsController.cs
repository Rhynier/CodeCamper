﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using CodeCamper.Model;

namespace CodeCamper.Web.Controllers
{
    public class LookupsController : ApiControllerBase
    {
        public LookupsController(ICodeCamperUow dataService)
        {
            DataService = dataService;
        }

        // GET: api/lookups/rooms
        [ActionName("rooms")]
        public IQueryable<Room> GetRooms()
        {
            return DataService.Rooms.GetAll().OrderBy(r => r.Name);
        }

        // GET: api/lookups/timeslots
        [ActionName("timeslots")]
        public IQueryable<TimeSlot> GetTimeSlots()
        {
            return DataService.TimeSlots.GetAll().OrderBy(ts => ts.Start);
        }

        // GET: api/lookups/tracks
        [ActionName("tracks")]
        public IQueryable<Track> GetTracks()
        {
            return DataService.Tracks.GetAll().OrderBy(t => t.Name);
        }

        // Lookups: aggregates the many little lookup lists in one payload
        // to reduce roundtrips when the client launches.
        // GET: api/lookups
        //[ActionName(Names.Actions.Lookups)]
        public Lookups GetLookups()
        {
            var lookups = new Lookups
            {
                Rooms = GetRooms().ToList(),
                TimeSlots = GetTimeSlots().ToList(),
                Tracks = GetTracks().ToList(),
            };
            return lookups;
        }
    }
}
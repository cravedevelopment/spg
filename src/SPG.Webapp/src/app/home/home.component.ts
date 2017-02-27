import { Component, OnInit } from '@angular/core';

import { Event } from '../models/event';
import {  EventService } from '../event/event.service';

@Component({
    moduleId: module.id,
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {
    events: Event[] = [];

    constructor(private eventService: EventService) { }

    ngOnInit() {
        // get events from secure api end point
        this.eventService.getUsers()
            .subscribe(events => {
                this.events = events;
            });
    }

}
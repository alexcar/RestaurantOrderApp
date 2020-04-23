import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { OrderService } from './services/order.service';
import { TimeOfDay } from './services/timeOfDay';
import { Order } from './services/order';
import { orderRequestValidator } from './validators/orderRequest-validator';
import { OrderHistory } from './services/orderHistory';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Restaurant Order App';
  form: FormGroup;
  timesOfDay: TimeOfDay[];
  order: Order = new Order();
  response: any;
  outputDishes: string;
  orderHistory: OrderHistory[] = [];

  constructor(private service: OrderService) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      orderRequest: new FormControl('', [
        orderRequestValidator,
        Validators.pattern('[a-zA-Z0-9, ]*'),
        Validators.required
      ]),
      outputMessage: new FormControl('')
    });

    this.service.getTimeOfDay()
      .subscribe( data => {
        this.timesOfDay = data;
      });
  }

  onSubmit() {
    // validators
    const orderRequest  = this.form.get('orderRequest').value.split(',');
    const timeOfDayRequest: string = orderRequest[0];

    let timeOfDay: string;

    for (let i in this.timesOfDay) {
      if (this.ciEquals(timeOfDayRequest, this.timesOfDay[i].name)) {
        timeOfDay = this.timesOfDay[i].name;
      }
    }

    if (timeOfDay) {
      this.order.timeOfDay = timeOfDay;

      for (let i = 1; i < orderRequest.length; i++) {
        this.order.dishes.push(+orderRequest[i]);
      }
    } else {
      // Inform that the time of day is wrong
      this.form.get('orderRequest').setErrors({ orderRequestValidator: true });
    }

    // send to api server
    this.service.sendOrder(this.order)
      .subscribe(data => {
        this.response = data;

        if (this.response.success) {
          this.form.get('outputMessage').setValue(this.response.foods);

          const orderHistoryResponse = {} as OrderHistory;
          orderHistoryResponse.input = this.form.get('orderRequest').value;
          orderHistoryResponse.output = this.response.foods;
          orderHistoryResponse.message = this.response.message;

          this.orderHistory.push(orderHistoryResponse);
        } else {
          console.error(this.response.message);
        }
      });

    this.order.timeOfDay = '';
    this.order.dishes = new Array();
  }

  ciEquals(a, b) {
    return typeof a === 'string' && typeof b === 'string'
        ? a.localeCompare(b, undefined, { sensitivity: 'accent' }) === 0
        : a === b;
  }
}

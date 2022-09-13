import React, { Component } from 'react';

export class NoBalance extends Component {
  static displayName = NoBalance.name;

  render() {
    return (
      <>
        <tr>
          <td><h1>Not enough funds on the balance</h1></td>
          <td><h2>Your balance: @Model.Balance</h2></td>
          <td><h2>To top up your balance, go to your <a href='cart'>shopping cart</a>.</h2></td>
        </tr>
      </>
    );
  }
}

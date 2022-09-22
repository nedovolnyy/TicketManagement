import React, { Component, Fragment } from 'react'
import { Container } from 'reactstrap'
import { NavMenu } from './NavMenu'

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <Fragment>
          <NavMenu />
          <Container>
            <div>
            {this.props.children}</div>
          </Container>
      </Fragment>
    );
  }
}

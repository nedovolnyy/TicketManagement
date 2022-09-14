import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { withTranslation } from 'react-i18next';
import { LoginMenu } from '../components/LoginMenu';
import { Link } from 'react-router-dom';
import './NavMenu.css';

class NavMenuPlain extends Component {
  static displayName = NavMenuPlain.name;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      hasAdminRole: false,
      collapsed: true
    };
  }


  componentDidMount() {
    //this._subscription = authService.subscribe(() => this.populateState());
    this.populateState();
  }

  async populateState() {
    /*const hasAdminRole = await authService.hasRole(UserRoles.Administrator);
    this.setState({
      hasAdminRole
    });*/
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }
  
  render() {
    const { t } = this.props;
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">TicketManagement</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/fetch-data">{t('Upload Event from file:')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/AreasManagement">{t('Areas management')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/LayoutsManagement">{t('Layouts management')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/VenuesManagement">{t('Venues management')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/UsersManagement">{t('Users management')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/cart">{t('Cart')}</NavLink>
                </NavItem>
                {
                  //this.state.hasAdminRole &&
                  <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/users">{t('Users')}</NavLink>
                  </NavItem>
                }
                <LoginMenu />
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}

export const NavMenu = withTranslation()(NavMenuPlain);

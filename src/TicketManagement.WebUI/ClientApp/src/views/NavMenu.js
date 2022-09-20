import React, { Component } from 'react'
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Form, Label, Input } from 'reactstrap'
import { withTranslation } from 'react-i18next'
import { LoginMenu } from '../components/LoginMenu'
import { Link, Route, useNavigate } from 'react-router-dom'
import { ROLES } from '../App'
import './NavMenu.css'
import RequireRole from '../helpers/RequireRole'
import { DataNavigation } from 'react-data-navigation'
import { ThirdPartyEvents } from './ThirdPartyEvents/Preview'
import { withRouter } from '../helpers/withRouter'
import { Auth } from '../helpers/Auth'

class NavMenuPlain extends Component {
  static displayName = NavMenuPlain.name;

  constructor(props) {
    super(props)
    this.state = { collapsed: true, thirdPartyEvents: [] }
  }

  toggleNavbar = () => {
    this.setState({ collapsed: !this.state.collapsed });
  }

  getThirdPartyEvents(event) {
    const reader = new FileReader();
    reader.onload = (event) => {
      this.setState({ thirdPartyEvents: JSON.parse(event.target.result) })
    }
    reader.readAsText(event.target.files[0]);
  }

  static handleSelectFile = (event, that) => {
    console.log(that.state.thirdPartyEvents);
    that.props.router.navigate('ThirdPartyEvents/Preview', { thirdPartyEvent: that.state.thirdPartyEvents })
    //<Route path="ThirdPartyEvents/Preview" element={<ThirdPartyEvents thirdPartyEvent={this.state.thirdPartyEvents} />}></Route>
    //DataNavigation.setData('ThirdPartyEvents', thirdPartyEvent);
    //navigate("ThirdPartyEvents/Preview");
  }

  render() {
    const { t } = this.props;
    return (
      <header>
        <Navbar className='navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3' light>
          <NavbarBrand tag={Link} to='/'>TicketManagement</NavbarBrand>
          <NavbarToggler onClick={this.toggleNavbar} className='mr-2' />
          <Collapse className='d-sm-inline-flex flex-sm-row-reverse' isOpen={!this.state.collapsed} navbar>
            <ul className='navbar-nav flex-grow'>
              {this.props.auth?.roles?.find(role => [ROLES.Administrator, ROLES.EventManager].includes(role)) &&
                <>
                  <li className='nav-item e-upload e-control-wrapper e-lib e-keyboard'>
                    <form /*'ThirdPartyEvents/Preview'*/ method='post' encType='multipart/form-data'>
                      <Label>{t('Upload Event from file:')}</Label>
                      <Input type='file' name='file' accept='.json' onChange={(event) => (this.getThirdPartyEvents, NavMenuPlain.handleSelectFile(event, this))} className='inputFileStyle' /> {/* todo */}
                    </form>
                  </li>
                  <NavItem className='nav-item'>
                    <NavLink tag={Link} className='text-dark' to='/EventsManagement/SelectVenues'>{t('Create event')}</NavLink>
                  </NavItem>
                </>
              }
              {this.props.auth?.roles?.find(role => [ROLES.Administrator].includes(role)) &&
                <>
                  <NavItem>
                    <NavLink tag={Link} className='text-dark' to='/AreasManagement'>{t('Areas management')}</NavLink>
                  </NavItem>
                  <NavItem>
                    <NavLink tag={Link} className='text-dark' to='/LayoutsManagement'>{t('Layouts management')}</NavLink>
                  </NavItem>
                  <NavItem>
                    <NavLink tag={Link} className='text-dark' to='/VenuesManagement'>{t('Venues management')}</NavLink>
                  </NavItem>
                  <NavItem>
                    <NavLink tag={Link} className='text-dark' to='/UsersManagement'>{t('Users management')}</NavLink>
                  </NavItem>
                </>
              }
              {this.props.auth?.roles?.find(role => [ROLES.Administrator, ROLES.EventManager, ROLES.User].includes(role)) &&
                <NavItem>
                  <NavLink tag={Link} className='text-dark' to='/cart'>{t('Cart')}</NavLink>
                </NavItem>
              }
              <LoginMenu />
            </ul>
          </Collapse>
        </Navbar>
      </header>
    );
  }
}

export const NavMenu = Auth(withRouter(withTranslation()(NavMenuPlain)));

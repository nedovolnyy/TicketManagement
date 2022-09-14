import React, { Component } from 'react';
import { withTranslation } from 'react-i18next';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { LanguageSelect } from '../components/LanguageSelect';

class LayoutPlain extends Component {
  static displayName = LayoutPlain.name;

  render() {
    const { t } = this.props;
    return (
      <div>
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
        <footer className="border-top footer text-muted">
          <Container>
            <div className="row justify-content-between">
              <div className="col-4">
                &copy; {new Date().getFullYear()} - {t('EPAM Spring-Summer Laboratory')} <a href="https://github.com/EPAM-Gomel-NET-Lab/ArtsiomKrot">GitHub</a>
              </div>
              <div className="col-4">
                <LanguageSelect />
              </div>
            </div>
          </Container>
        </footer>
      </div>
    );
  }
}

export const Layout = withTranslation()(LayoutPlain);

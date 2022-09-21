import React from 'react'
import { withTranslation } from 'react-i18next'
import { Container } from 'reactstrap';
import { LanguageSelect } from '../components/LanguageSelect';

const FooterPlain = (props) => {
  const { t } = props;
  return (
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
  );
}

export const Footer = withTranslation()(FooterPlain);

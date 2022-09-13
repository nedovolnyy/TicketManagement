import React from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './views/Layout';
import { useTranslation } from 'react-i18next';
import useLocalStorage from './hooks/useLocalStorage';
import i18n from './i18n';
import './custom.css';

export default function App() {
  const { t } = useTranslation();
  const [language, setLanguage] = useLocalStorage('language', 'ru');

  const handleLanguageChange = () => {
    if (language === 'en') {
      i18n.changeLanguage('ru');
      setLanguage('ru');
    } else if (language === 'ru') {
      i18n.changeLanguage('en');
      setLanguage('en');
    }
  };

  return (
      <>
        <button onClick={handleLanguageChange}>
          {language === 'ru' ? t('english') : t('russian')}
        </button>
        <button className='reload' onClick={() => window.location.reload()}>
          {('refresh page')}
        </button>
    <Layout>
      <Routes>
        {AppRoutes.map((route, index) => {
          const { element, ...rest } = route;
          return <Route key={index} {...rest} element={element} />;
        })}
      </Routes>
    </Layout>
      </>
  );
}

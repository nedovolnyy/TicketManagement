import { Counter } from "./views/Counter";
import  Cart  from "./views/Cart";
import { NoBalance } from "./views/NoBalance";
import { Home } from "./views/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/cart',
    element: <Cart />
  },
  {
    path: '/nobalance',
    element: <NoBalance />
  }
];

export default AppRoutes;

import { Counter } from "./views/Counter";
import { Home } from "./views/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  }
];

export default AppRoutes;

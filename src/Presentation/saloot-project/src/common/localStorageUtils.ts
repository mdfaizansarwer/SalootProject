const setItem = (key: string, value: any): void => {
  const parsedValue = JSON.stringify(value);
  localStorage.setItem(key, parsedValue);
};

const getItem = (key: string): any => {
  const item = localStorage.getItem(key) || "{}";
  const parsedValue = JSON.parse(item);
  return parsedValue;
};

const removeItem = (key: string): void => {
  localStorage.removeItem(key);
};

const updateItem = (key: string, value: any): void => {
  removeItem(key);
  setItem(key, value);
};

export default {
  setItem,
  getItem,
  removeItem,
  updateItem,
};

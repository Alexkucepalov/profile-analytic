const API_BASE = 'http://192.168.90.116:5000/home';

export async function getContrpartnerByName(name: string) {
  const res = await fetch(`${API_BASE}/GetContrpartnerByName?name=${encodeURIComponent(name)}`);
  const data = await res.json();
  // Вернуть массив объектов value, если есть
  return Array.isArray(data) ? data.map(item => item.value) : [];
}

export async function getContrpartnerFirst() {
  const res = await fetch(`${API_BASE}/GetContrpartnerFirst`);
  return await res.json();
}

export async function getTnsByContrpartner(id: number) {
  const res = await fetch(`${API_BASE}/GetTnsByContrpartner?id=${id}`);
  return await res.json();
}

export async function getContrpartnerByInn(inn: string) {
  const res = await fetch(`${API_BASE}/GetContrpartnerByInn?inn=${encodeURIComponent(inn)}`);
  const data = await res.json();
  return Array.isArray(data) ? data.map(item => item.value) : [];
}

export async function getTnsByMonths(id: number) {
  const res = await fetch(`${API_BASE}/GetTnsByMonths?id=${id}`);
  return await res.json();
}

export async function getTnsBySuppliers(id: number) {
  const res = await fetch(`${API_BASE}/GetTnsBySuppliers?id=${id}`);
  return await res.json();
}

export async function getAssortments(id: number) {
  const res = await fetch(`${API_BASE}/GetAssortments?id=${id}`);
  return await res.json();
}

export async function getSaleDocumentsByContrpartner(id: number) {
  const res = await fetch(`${API_BASE}/GetSaleDocumentsByContrpartner?id=${id}`);
  return await res.json();
}

export async function getFrequentlyAssortment(id: number) {
  const res = await fetch(`${API_BASE}/GetFrequentlyAssortment?id=${id}`);
  return await res.json();
}

export async function getFrequentlyAssortmentByContrpartner(id: number) {
  const res = await fetch(`${API_BASE}/GetFrequentlyAssortmentByContrpartner?id=${id}`);
  const text = await res.text();
  if (!text) return [];
  return JSON.parse(text);
}

export async function getAprioriAssortment(id: number) {
  const res = await fetch(`${API_BASE}/GetAssortmentApriori?id=${id}`);
  const text = await res.text();
  if (!text) return [];
  return JSON.parse(text);
}


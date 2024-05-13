export const exerciseOptions =  {
    method: 'GET',
  params: {limit: '10'},
  headers: {
    'X-RapidAPI-Key': '056438a862msh4eecb0a045f86dfp12dc94jsn9a90e383df02',
    'X-RapidAPI-Host': 'exercisedb.p.rapidapi.com'
}
};
export const fetchData = async (url,options) => {
    const response = await fetch(url, options);
    const data = await response.json();

    return data;
}
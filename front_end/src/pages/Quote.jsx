import { Link } from 'react-router-dom';

export default function Quote() {
  return (
	<div>
	  <h1>Quote</h1>
	  <p>Quote form goes here.</p>
	  <Link to="/results">See results</Link>
	</div>
  );
}


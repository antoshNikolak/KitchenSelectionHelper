import { Link } from 'react-router-dom';

export default function Result() {
  return (
	<div>
	  <h1>Results</h1>
	  <p>Your estimate will appear here.</p>
	  <Link to="/">Back home</Link>
	</div>
  );
}


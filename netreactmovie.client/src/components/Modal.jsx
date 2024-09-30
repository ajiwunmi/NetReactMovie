import "../assets/css/Modal.css"; // CSS for the modal styling

const Modal = ({ children, onClose }) => {
	return (
		<div className="modal-backdrop" onClick={onClose}>
			<div className="modal-content" onClick={(e) => e.stopPropagation()}>
				<button className="close-button" onClick={onClose}>
					X
				</button>
				{children}
			</div>
		</div>
	);
};

export default Modal;

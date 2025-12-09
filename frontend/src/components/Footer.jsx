import React from "react";

function classNames(...classes) {
  return classes.filter(Boolean).join(" ");
}

const Footer = () => {
  return (
    <footer className="relative bg-gray-800 after:pointer-events-none after:absolute after:inset-x-0 after:bottom-0 after:h-px after:bg-white/10">
      <div className="w-full mx-auto max-w-7xl p-4 md:flex md:items-center md:justify-between">
        <span className="text-gray-300 rounded-md px-3 py-2 text-sm font-medium">
          © 2025 . All Rights Reserved.
        </span>
        <ul className="flex flex-wrap items-center mt-3 text-sm font-medium text-body sm:mt-0">
          <li>
            <a
              href="#"
              className="text-gray-300 hover:bg-white/5 hover:text-white
                rounded-md px-3 py-2 text-sm font-medium"
            >
              About
            </a>
          </li>
          <li>
            <a
              href="#"
              className="text-gray-300 hover:bg-white/5 hover:text-white
                rounded-md px-3 py-2 text-sm font-medium"
            >
              Privacy Policy
            </a>
          </li>
          <li>
            <a
              href="#"
              className="text-gray-300 hover:bg-white/5 hover:text-white
                rounded-md px-3 py-2 text-sm font-medium"
            >
              Licensing
            </a>
          </li>
          <li>
            <a
              href="#"
              className="text-gray-300 hover:bg-white/5 hover:text-white
                rounded-md px-3 py-2 text-sm font-medium"
            >
              Contact
            </a>
          </li>
        </ul>
      </div>
    </footer>
  );
};

export default Footer;

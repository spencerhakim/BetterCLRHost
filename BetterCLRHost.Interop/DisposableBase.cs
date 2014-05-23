/*
 * Copyright 2011 Microsoft Corporation
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy
 * of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 * ----------------------------------------------------------------------------
 * Modified by Spencer Hakim
 */

namespace CLROBS
{
    using System;
    using System.Threading;

    /// <summary>
    /// DisposableBase class. Represents an implementation of the IDisposable interface.
    /// </summary>
    public abstract class DisposableBase : IDisposable
    {
        /// <summary>
        /// A value which indicates the disposable state. 0 indicates undisposed, 1 indicates disposing
        /// or disposed.
        /// </summary>
        private long disposableState;

        /// <summary>
        /// Finalizes an instance of the DisposableBase class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Implementation is correct, semantics improved.")]
        ~DisposableBase()
        {
            // The destructor has been called as a result of finalization, indicating that the object
            // was not disposed of using the Dispose() method. In this case, call the Dispose(bool)
            // method with the disposeManagedResources flag set to false, indicating that derived classes
            // may only release unmanaged resources.
            this.Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether the object is undisposed.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Undisposed", Justification = "Implementation is correct, semantics improved. Derived classes may use this property to determine whether the object is undisposed, meaning not currently disposing, or previously disposed.")]
        public bool IsUndisposed
        {
            get
            {
                return Thread.VolatileRead(ref this.disposableState) == 0;
            }
        }

        /// <summary>
        /// Disposes of the specified set of resources.
        /// </summary>
        /// <param name="disposables">The set of resources to dispose of.</param>
        public void DisposeOf(params IDisposable[] disposables)
        {
            long state = Thread.VolatileRead(ref this.disposableState);                
            if (state == 1)
            {
                foreach (IDisposable disposable in disposables)
                {
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with disposing of resources.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Implementation is correct, semantics improved.")]
        public void Dispose()
        {
            // Attempt to move the disposable state from 0 to 1. If successful, we can be assured that
            // this is the first time Dispose has been called.
            if (Interlocked.CompareExchange(ref this.disposableState, 1, 0) == 0)
            {
                // Call the Dispose(bool) method with the disposeManagedResources flag set to true, indicating
                // that derived classes may release unmanaged resources and dispose of managed resources.
                this.Dispose(true);

                Interlocked.Exchange(ref this.disposableState, 2);

                // Suppress finalization of this object (remove it from the finalization queue and
                // prevent the destructor from being called).
                GC.SuppressFinalize(this);
            }
        }

        #endregion IDisposable Members

        /// <summary>
        /// Dispose resources. Override this method in derived classes. Unmanaged resources should always be released
        /// when this method is called. Managed resources may only be disposed of if disposeManagedResources is true.
        /// </summary>
        /// <param name="disposeManagedResources">A value which indicates whether managed resources may be disposed of.</param>
        protected abstract void Dispose(bool disposeManagedResources);
    }
}
